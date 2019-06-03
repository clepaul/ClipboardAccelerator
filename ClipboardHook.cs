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
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ClipboardAccelerator
{
    // Removed "public" in the "ClipboardHook" definition
    static class ClipboardHook
    {
        // Places the given window in the system-maintained clipboard format listener list.
        // Source: http://www.fluxbytes.com/csharp/how-to-monitor-for-clipboard-changes-using-addclipboardformatlistener/
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);


        // Removes the given window from the system-maintained clipboard format listener list.
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        public static void InstallHook(IntPtr hwnd)
        {
            AddClipboardFormatListener(hwnd);
            //MessageBox.Show("Hook Installed");
        }

        //public static void OnWindowClosing(object sender, CancelEventArgs e)
        public static void OnWindowClosing()
        {
            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            RemoveClipboardFormatListener(windowHandle);
            //MessageBox.Show("Hook removed");
        }

    }



}
