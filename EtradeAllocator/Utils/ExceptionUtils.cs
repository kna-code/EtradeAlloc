using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EtradeAllocator.Utils
{
    public class ExceptionUtils
    {
        public static string GetLogText(Exception e)
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendFormat("Exception Occured: ({0}): {1}", e.GetType().Name, e.Message);
            strBuilder.AppendLine();
            strBuilder.AppendLine(e.StackTrace);

            int index = 1;
            Exception inner = e.InnerException;
            while (inner != null)
            {
                strBuilder.AppendFormat("Inner Exception #{2}: ({0}): {1}", e.GetType().Name, e.Message, index);
                strBuilder.AppendLine();
                strBuilder.AppendLine(e.StackTrace);
                inner = inner.InnerException;
                ++index;
            }

            return strBuilder.ToString();

        }

    }
}
