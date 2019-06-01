using System;
using System.Text;

namespace ClipboardAccelerator
{
    // Take care of the program log data
    static class Logger
    {
        public static StringBuilder sbLogstring { get; private set; }


        static Logger() => sbLogstring = new StringBuilder();

        public static void WriteLog(string stringtoadd)
        {
            sbLogstring.Append(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " => " + stringtoadd + Environment.NewLine);
        }
    }
}