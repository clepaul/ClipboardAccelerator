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
