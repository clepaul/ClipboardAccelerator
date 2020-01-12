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
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClipboardAccelerator
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        static public bool bIsFirstWindow { get; private set; } = true;

        private bool InConfigMode = false;

        public NotificationWindow()
        {           
            InitializeComponent();
            bIsFirstWindow = false;

            // Setup a timer to close the NW automatically after X seconds
            // Source: http://stackoverflow.com/questions/11719283/how-to-close-auto-hide-wpf-window-after-10-sec-using-timer/11720080
            // Source: DispatcherTimer == timer on the same thread using windows messages -> http://stackoverflow.com/questions/1111645/comparing-timer-with-dispatchertimer
            DispatcherTimer timer = new DispatcherTimer();

            // TODO: document recommended notification window time = 3 seconds
            // TODO: make sure ther is a valid value in the uiNotificationWNDDelay setting
            timer.Interval = TimeSpan.FromMilliseconds(Properties.Settings.Default.uiNotificationWNDDelay);
            timer.Tick += TimerTick;
            timer.Start();
        }


        // This needs to be called to configure the position of the notification window
        public NotificationWindow(bool ConfigWindow)
        {
            InitializeComponent();

            // Make the "Done" button visible and change the text of the textblock
            bDone.Visibility = Visibility.Visible;
            tBMessage.Text = "Drag me to the desired location";

            InConfigMode = true;
        }

        // This function enables dragging using the whole client window
        // Source: https://social.msdn.microsoft.com/Forums/vstudio/en-US/2de6ca1d-446c-4f2d-8f0a-93b5482f8b56/how-to-move-the-whole-window-by-draging-the-client-area?forum=wpf
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        // Save the coordinates of the notification window and close the "config" window if the "Done" button is clicked
        private void bDone_Click(object sender, RoutedEventArgs e)
        {
            Point screenCoordinates = this.PointToScreen(new Point(0, 0));
            Properties.Settings.Default.dXNotifyWindow = screenCoordinates.X;
            Properties.Settings.Default.dYNotifyWindow = screenCoordinates.Y;
            Properties.Settings.Default.Save();
            
            InConfigMode = false;
            Close();
            
        }




        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (InConfigMode) { return;  }

            //Owner.Topmost = true;
            //Owner.WindowState = WindowState.Normal;
            //Owner.Focus();
            //Owner.Activate();
            //Owner.ShowActivated = false;
            //Owner.Show();
            //Owner.BringIntoView();

            // Activate and show (if minimized) the main CA window
            // Source: http://blog.binarybits.net/programming/bringing-window-to-front-in-wpf/

            if (!Owner.IsVisible)
            {
                Owner.Show();
            }
            Owner.WindowState = WindowState.Normal;
            //Owner.Activate();
            // ... Most likely the below is required. More testing required.
            Owner.Topmost = true;
            Owner.Topmost = false;
            //Owner.Focus();

            
            bIsFirstWindow = true;

            Close();
        }



        // Timer callback
        // Todo: is this thread save? E.g. is updating "bIsFirstWindow" safe?
        // Source: http://stackoverflow.com/questions/11719283/how-to-close-auto-hide-wpf-window-after-10-sec-using-timer/11720080
        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;

            Close();
            bIsFirstWindow = true;
        }
    }
}
