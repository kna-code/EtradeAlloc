using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EtradeAllocator.Data;

namespace EtradeAllocator.Etrade
{
    class EtradeParser
    {
        private readonly SymbolMapper m_SymbolMapper;

        public EtradeParser()
        {
            m_SymbolMapper = new SymbolMapper();
        }

        public Portfolio LoadPortfolioFromCsv(string filePath, DesiredPortfolio desiredPortfolio)
        {
            string[] fileLines = ReadFileLines(filePath);

            Portfolio portfolio = new Portfolio(desiredPortfolio);

            // Allocations begin at line #11
            const int allocationStartLine = 11;
            string[] splitStrings = new[] { "\",\"", "\",", ",\"", "\"", ";"};

            for (int index = allocationStartLine; index < fileLines.Length; ++index)
            {
                // Clean up the lines
                string line = fileLines[index];
                string[] lineFields = line.Split(splitStrings, StringSplitOptions.RemoveEmptyEntries); 

                // Try to parse them into the portfolio
                ParseHolding(lineFields, ref portfolio);
            }


            return portfolio;
        }

        private string[] ReadFileLines(string filePath)
        {
            // Any file opened in Excel is locked for opening by any other process.
            // To get arround this, first the source file to a temp path, load from it,
            // then cleanup.
            
            string tempPath = Path.GetTempFileName();
            try
            {
                // Copy to the temp file
                File.Copy(filePath, tempPath, true);

                // Load from it
                return File.ReadAllLines(tempPath);
            }
            finally
            {
                // Cleanup
                FileInfo tempFile = new FileInfo(tempPath);
                if (tempFile.Exists)
                {
                    tempFile.Delete();
                }
            }
        }

        private void ParseHolding(string[] lineFields, ref Portfolio portfolio)
        {
            switch(lineFields.Length)
            {
                case 2:
                    ParseCashHolding(lineFields, ref portfolio);
                    break;

                case 10:
                    ParseSecuritiesHolding(lineFields, ref portfolio);
                    break;
            };
        }

        private void ParseCashHolding(string[] lineFields, ref Portfolio portfolio)
        {
            string symbol = lineFields[0];
            decimal price = new decimal(1.0);
            decimal cashValue = Convert.ToDecimal(lineFields[1]);

            portfolio.AddHolding(AllocationType.Cash, symbol, price, cashValue);
        }

        private void ParseSecuritiesHolding(string[] lineFields, ref Portfolio portfolio)
        {
            string symbol = lineFields[0];
            decimal lastPrice = Convert.ToDecimal(lineFields[1]);
            decimal quantity = Convert.ToDecimal(lineFields[5]);

            portfolio.AddHolding(m_SymbolMapper.GetAllocationType(symbol), symbol, lastPrice, quantity);          
        }

    }
}
