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
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace ClipboardAccelerator
{
    public partial class MainWindow : Window
    {
        private void CheckIfAlreadyRunning()
        {
            int OwnProcessID = Process.GetCurrentProcess().Id;

            // Source: http://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path.Replace("/", "\\"));
            Logger.WriteLog($"Own instance: {path} Own process ID: {OwnProcessID}");

            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    if (p.Id == OwnProcessID) continue;

                    if (p.MainModule.FileName == path)
                    {
                        MessageBox.Show($"Clipboard Accelerator is already running: {Environment.NewLine}{p.MainModule.FileName} ID: {p.Id.ToString()}",
                                        "Note", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                        // Source: http://stackoverflow.com/questions/7146080/closing-applications
                        // Todo: is there another way to exit a WPF program and return an exit code?
                        Environment.Exit(1);
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
