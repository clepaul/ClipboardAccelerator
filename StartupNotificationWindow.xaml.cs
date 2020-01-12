/* Clipboard Accelerator - Executes commands with data from the clipboard
Copyright (C) 2016 - 2020  Clemens Paul

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>. */


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
