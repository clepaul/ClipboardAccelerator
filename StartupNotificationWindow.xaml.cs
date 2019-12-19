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
    /// <summary>
    /// Interaction logic for StartupNotificationWindow.xaml
    /// </summary>
    public partial class StartupNotificationWindow : Window
    {
        private bool? accepted = false;

        public StartupNotificationWindow()
        {
            InitializeComponent();

            Closing += StartupNotificationWindo_Closing;
        }

        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            if (SuppressStartupMsgCBox.IsChecked.Value)
            {                
                Properties.Settings.Default.ShowStartupWindow = false;                
                Properties.Settings.Default.Save();
            }
            accepted = true;
            this.Close();
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        void StartupNotificationWindo_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = accepted;            
        }
    }
}
