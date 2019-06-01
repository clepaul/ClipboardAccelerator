using System;
using System.Text;

namespace ClipboardAccelerator
{
    public class ClipboardEntry
    {
        public static int CBInView = -1;

        private StringBuilder sCBContent = new StringBuilder();

        public readonly string sCBTime = "";

        public ClipboardEntry(StringBuilder sb)
        {
            sCBContent = sb;
            sCBTime = DateTime.Now.ToShortTimeString();
        }

        public string GetCBText()
        {
            return sCBContent.ToString();
        }

        public StringBuilder GetCBTextAsStringBuilder()
        {
            return sCBContent;
        }
        
    }



}
