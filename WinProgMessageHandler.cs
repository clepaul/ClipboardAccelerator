/* This file is part of the source of "Clipboard Accelerator"
Copyright (C) 2016 - 2019  Clemens Paul

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
using System.Text;
using System.Windows;

namespace ClipboardAccelerator
{
    public partial class MainWindow : Window
    {
        // Handle the WinProg messages
        // NOTE: removed "static" from method "WinProc" to make GUI update "tbClipboardContent.Text = ..." work
        // Source: https://pingfu.net/csharp/2015/04/22/receive-wndproc-messages-in-wpf.html
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                if (cBIgnoreCBUpdate.IsChecked.Value) { return IntPtr.Zero; };


                try
                {
                    // Get Data from Clipboard
                    // Source: http://www.fluxbytes.com/csharp/how-to-monitor-for-clipboard-changes-using-addclipboardformatlistener/
                    IDataObject iData = Clipboard.GetDataObject();      // Clipboard's data.

                    // Todo: Send picture data in clipboard directly to mspaint
                    //if (iData.GetDataPresent(DataFormats.Bitmap)) { MessageBox.Show("Bitmap on clipboard");  }

                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        // Only copy the data from the clipboard if the timer is NOT running. E.g. to prevent multiple clipboard updates by applications like Excel which push the same data in multiple formats to the clipboard
                        if (ClipboardTimer.Enabled == false)
                        {
                            // Make sure the clipboard access delay is between 0 and 10 seconds
                            // TODO: document delay is between 0 and 10 seconds & recommended = 0.5 seconds
                            if (Properties.Settings.Default.dClipboardDelay >= 0 && Properties.Settings.Default.dClipboardDelay < 10001)
                            {
                                ClipboardTimer.Interval = Properties.Settings.Default.dClipboardDelay;
                            }
                            else
                            {
                                // Default clipboard delay is 0.5 seconds
                                ClipboardTimer.Interval = 500;
                            }

                            ClipboardTimer.AutoReset = false;
                            ClipboardTimer.Enabled = true;

                            // Todo: Setup logic to set user defined limit of CB lenth
                            StringBuilder text = new StringBuilder();
                            int iCBTextLenth = 0;

                            text.Append(iData.GetData(DataFormats.Text));


                            // TODO: Make sure that the (int) cast below does not introduce a bug, e.g. uiClipDisplaySize could be to big to fit into a regular "int"
                            // INT check done in "SettingsWindow.xaml.cs" - TODO: change uint to int so that it is not possible to change in the config file manually
                            if (text.Length > Properties.Settings.Default.uiClipDisplaySize) iCBTextLenth = (int)Properties.Settings.Default.uiClipDisplaySize;
                            else iCBTextLenth = text.Length;


                            // Combine clipboards if checkbox is checked
                            // Todo: Enable clipboard history functionality <-- check if this makes sense
                            if (cBCombineClipboard.IsChecked.Value)
                            {
                                if (tbClipboardContent.LineCount == 1)
                                {
                                    if (tbClipboardContent.GetLineLength(0) == 0)
                                    {
                                        tbClipboardContent.AppendText(text.ToString(0, iCBTextLenth));
                                    }
                                    else
                                    {
                                        tbClipboardContent.AppendText(Environment.NewLine + text.ToString(0, iCBTextLenth));
                                    }
                                }
                                else
                                {
                                    tbClipboardContent.AppendText(Environment.NewLine + text.ToString(0, iCBTextLenth));
                                }
                            }
                            else
                            {
                                // Compare the captured clipboard to the recent clipboard to prevent multiple clipboards with the same data
                                if (lClipboardList.Count > 0)
                                {
                                    if (text.Equals(lClipboardList[lClipboardList.Count - 1].GetCBTextAsStringBuilder()))
                                    {
                                        Logger.WriteLog("Ignoring clipboard update because the current clipboard data is identical to the last saved one.");
                                        return IntPtr.Zero;
                                    }
                                }


                                tbClipboardContent.Text = text.ToString(0, iCBTextLenth);


                                lClipboardList.Add(new ClipboardEntry(text));
                                ClipboardEntry.CBInView = lClipboardList.Count - 1;

                                if (lClipboardList.Count > 1) { bPrev.IsEnabled = true; }
                                bNext.IsEnabled = false;
                                bDeleteClipboardEntry.IsEnabled = true;


                                // Set the clipboard information (time of capture and number of visible clipboard) of the clipboard which was just captured
                                SetCBInfoString();


                                Logger.WriteLog("Captured clipboard: " + lClipboardList.Count.ToString());


                                // Hide the clipboard window if checkbox is checked
                                if (cBHideClipboard.IsChecked.Value) bShowClipboard.Visibility = Visibility.Visible;


                                // Add text in comboOptArg to the list of comboOptArg and clear the text
                                if (comboOptArg.Text != "")
                                {
                                    bool bInList = false;
                                    foreach (var item in comboOptArg.Items)
                                    {
                                        if (item.ToString() == comboOptArg.Text) bInList = true;
                                    }

                                    if (!bInList)
                                    {
                                        comboOptArg.Items.Add(comboOptArg.Text);
                                    }
                                    comboOptArg.Text = "";
                                }
                            }


                            // Show the clipboard changed notification window
                            // Source: http://stackoverflow.com/questions/7373335/how-to-open-a-child-windows-under-parent-window-on-menu-item-click-in-wpf
                            if (NotificationWindow.bIsFirstWindow == true && cBShowNW.IsChecked.Value)
                            {
                                NotificationWindow nw = new NotificationWindow();
                                nw.ShowInTaskbar = false;
                                nw.Left = Properties.Settings.Default.dXNotifyWindow; // Todo: Check this value - make sure it is in a valid range. E.g. 0 -> max screen size
                                nw.Top = Properties.Settings.Default.dYNotifyWindow; // Todo: Check this value - make sure it is in a valid range. E.g. 0 -> max screen size
                                nw.Topmost = true;
                                nw.Owner = Application.Current.MainWindow;
                                nw.Show();
                            }
                        }
                        // Clipboard timer is running -> no data will be copied from clipboard
                        else
                        {
                            Logger.WriteLog("Ignoring clipboard update because of the clipboard access delay (see Advanced Settings for details).");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLog("Failed to get data from Clipboard. Error: " + e.Message);
                    return IntPtr.Zero;
                }
            }
            return IntPtr.Zero;
        }
    }
}
