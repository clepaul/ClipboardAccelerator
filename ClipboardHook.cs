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
