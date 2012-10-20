using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EtradeAllocator.Data;
using EtradeAllocator.Etrade;
using EtradeAllocator.Trading;
using EtradeAllocator.Utils;

namespace EtradeAllocator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            LoadSettings();
        }

        private void LoadSettings()
        {
            m_FileTextBox.Text = Properties.Settings.Default.LastFile;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.LastFile = m_FileTextBox.Text;

            Properties.Settings.Default.Save();
        }

        private void m_UpdateButton_Click(object sender, EventArgs e)
        {
            string filePath = m_FileTextBox.Text;

            try
            {
                Portfolio portfolio = new EtradeParser().LoadPortfolioFromCsv(filePath, new DesiredPortfolio());
                RebalanceOrder order = new RebalanceOrder(portfolio);
                
                UpdatePortfolioDisplay(portfolio);
                UpdateTradingDisplay(order);

                SaveSettings(); // Only save when everything succeeded
            }
            catch(Exception exception)
            {
                string msg = String.Format("Unable to parse the E*Trade file from '{0}'\n\n{1}.", 
                    filePath, ExceptionUtils.GetLogText(exception));

                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void UpdatePortfolioDisplay(Portfolio portfolio)
        {
            m_TotalValueTextBox.Text = string.Format("{0:C}", portfolio.TotalValue);

            m_PortfolioDataGrid.DataSource = portfolio.Holdings;
        
            // Setup the Grid
            m_PortfolioDataGrid.Columns["Price"].DefaultCellStyle.Format = "c";
            m_PortfolioDataGrid.Columns["Value"].DefaultCellStyle.Format = "c";
            m_PortfolioDataGrid.Columns["Alloc"].DefaultCellStyle.Format = "p";
            m_PortfolioDataGrid.Columns["DesiredAlloc"].DefaultCellStyle.Format = "p";
            m_PortfolioDataGrid.Columns["AllocDiff"].DefaultCellStyle.Format = "p";
        }

        public void UpdateTradingDisplay(RebalanceOrder order)
        {
            m_TradesDataGridView.DataSource = order.Orders;

            // Setup the Grid
            m_TradesDataGridView.Columns["Value"].DefaultCellStyle.Format = "c";
        }
    }
}
