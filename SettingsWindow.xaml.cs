/* This file is part of the source of "Clipboard Accelerator"
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // Initialize checkboxes and values in textboxes
            cbEnableFirstLineOnly.IsChecked = Properties.Settings.Default.bEnableFirstLineOnly;
            cbHideFromTaskbarOnMinimize.IsChecked = Properties.Settings.Default.bHideFromTaskbarOnMinimize;
            tbLineCount.Text = Properties.Settings.Default.uiExecutionWarningCount.ToString();
            tbCopyClipboardBytes.Text = Properties.Settings.Default.uiClipDisplaySize.ToString();
            tbClipboardDelay.Text = Properties.Settings.Default.dClipboardDelay.ToString();
            tbNotificationWNDDelay.Text = Properties.Settings.Default.uiNotificationWNDDelay.ToString();
        }




        // TODO: Move the below logic of the three functions into the "bOK_Click" function
        private void cbEnableFirstLineOnly_Checked(object sender, RoutedEventArgs e)
        {
            CheckboxHandler(sender as CheckBox);
        }


        private void cbEnableFirstLineOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckboxHandler(sender as CheckBox);
        }


        void CheckboxHandler(CheckBox theCheckBox)
        {
            // Todo: "Properties.Settings.Default.bEnableFirstLineOnly" should be always set to "true" when the application re-starts
            Properties.Settings.Default.bEnableFirstLineOnly = theCheckBox.IsChecked.Value;
            Properties.Settings.Default.Save();            
        }





        private void bOK_Click(object sender, RoutedEventArgs e)
        {           
            uint tmpInt = 0;
            double tmpDouble = 0;

            // Set the "uiExecutionWanringCount" setting to the value in the "tbLineCount" textbox
            if (!uint.TryParse(tbLineCount.Text, out tmpInt))
            {
                Properties.Settings.Default.uiExecutionWarningCount = 10;
            }
            else
            {
                if (tmpInt > int.MaxValue) tmpInt = int.MaxValue;
                Properties.Settings.Default.uiExecutionWarningCount = tmpInt;
            }


            // Set the "uiClipDisplaySize" setting to the value in the "tbCopyClipboardBytes" textbox
            tmpInt = 0;
            if (!uint.TryParse(tbCopyClipboardBytes.Text, out tmpInt))
            {
                Properties.Settings.Default.uiClipDisplaySize = 500000;
            }
            else
            {
                if(tmpInt > int.MaxValue) tmpInt = int.MaxValue;
                Properties.Settings.Default.uiClipDisplaySize = tmpInt;                
            }


            // Set the "dClipboardTimeout" setting to the value in the "tbClipboardDelay" textbox
            // Timer can't be bigger than int32.MaxValue => https://msdn.microsoft.com/de-de/library/system.timers.timer.interval(v=vs.110).aspx       
            if (!double.TryParse(tbClipboardDelay.Text, out tmpDouble))
            {
                Properties.Settings.Default.dClipboardDelay = 500;
            }
            else
            {
                if (tmpDouble > int.MaxValue) tmpDouble = int.MaxValue;
                Properties.Settings.Default.dClipboardDelay = tmpDouble;
            }


            // Set the "uiNotificationWNDDelay" setting to the value in the "tbCopyClipboardBytes" textbox
            tmpInt = 0;
            if (!uint.TryParse(tbNotificationWNDDelay.Text, out tmpInt))
            {
                Properties.Settings.Default.uiNotificationWNDDelay = 1000;
            }
            else
            {
                if (tmpInt > int.MaxValue) tmpInt = int.MaxValue;
                Properties.Settings.Default.uiNotificationWNDDelay = tmpInt;
            }


            // Set the "uiCommandsFontSize" setting to the value in the "cBCommandsFontSize" combobox
            tmpInt = 0;
            if (!uint.TryParse(cBCommandsFontSize.Text, out tmpInt))
            {
                Properties.Settings.Default.uiCommandsFontSize = 30;
            }
            else
            {
                // 72 should be the max font size
                if (tmpInt > 72) tmpInt = 72;
                Properties.Settings.Default.uiCommandsFontSize = tmpInt;
            }


            // Get the current setting of the checkbox
            Properties.Settings.Default.bHideFromTaskbarOnMinimize = cbHideFromTaskbarOnMinimize.IsChecked.Value;

            Properties.Settings.Default.Save();
            this.Close();
        }


        // Verify the text to be numeric
        // Source: http://stackoverflow.com/questions/14813960/how-to-accept-only-integers-in-a-wpf-textbox
        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }


        // Verify the text to be numeric
        // Source: http://stackoverflow.com/questions/14813960/how-to-accept-only-integers-in-a-wpf-textbox
        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(), delegate (Char c) { return Char.IsDigit(c) || Char.IsControl(c); });
        }


        // Verify the pasted input is numeric
        // Source: http://stackoverflow.com/questions/14813960/how-to-accept-only-integers-in-a-wpf-textbox
        private void tb_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            for (int i = 8; i <= 72; i = i + 2)
            {
                cBCommandsFontSize.Items.Add(i.ToString());
            }
            cBCommandsFontSize.Text = Properties.Settings.Default.uiCommandsFontSize.ToString();
        }

        private void bSetNotificationWindowPos_Click(object sender, RoutedEventArgs e)
        {
            // Todo: make sure only one instance of this can be opened at the same time. E.g. static class variable of the "NotificationWindow" class that indicates that one is running
            // "true" will tell the NotificationWindow that it is in config mode.
            NotificationWindow nw = new NotificationWindow(true);
            nw.ShowInTaskbar = false;
            nw.Left = 0;
            nw.Top = 0;
            nw.Topmost = true;
            nw.Owner = Application.Current.MainWindow;
            nw.Show();
        }
    }
}
