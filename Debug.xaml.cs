using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClipboardAccelerator
{
    public partial class Debug : Window
    {
        public static bool IsFirstWindow { get; private set; } = true;
        public static Debug TheDebugWindow { get; private set; } = null;

        public Debug()
        {
            InitializeComponent();

            if (!IsFirstWindow) Close();

            IsFirstWindow = false;
            TheDebugWindow = this;
        }
              

        private void bRefresh_Click(object sender, RoutedEventArgs e)
        {
            tBDebug.Clear();
            tBDebug.AppendText(Logger.sbLogstring.ToString());                        
        }


        private void tBDebug_Initialized(object sender, EventArgs e)
        {
            tBDebug.Clear();
            tBDebug.AppendText(Logger.sbLogstring.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsFirstWindow = true;
            TheDebugWindow = null;
        }
    }
}
