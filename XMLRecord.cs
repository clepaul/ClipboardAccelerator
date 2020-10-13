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
using System.Windows;
using System.Xml.Linq;

namespace ClipboardAccelerator
{
    public class XMLRecord
    {
        public List<Tuple<string, string>> Options { get; private set; }              
        public string Executable { get; private set; }
        public string AllArguments { get; private set; }
        public string Class { get; private set; }
        public string Description { get; private set; }
        public string Path { get; private set; }
        public string ProgramID { get; private set; }
        public string CommandIsSafe { get; private set; }
        public string UsePipe { get; private set; }
        // TODO: document the following four XML parameters
        public string ShellExecute { get; private set; }
        public string IsDll { get; private set; }        
        public string DllNamespaceName { get; private set; }
        public string DllClassName { get; private set; }
        public string DllMethodName { get; private set; }
        public string DllConfigFilePath { get; private set; }
        // TODO: Create a public string var containing the path to the xml file using the below "sXmlPath" variable.
        //  This can be used later in the call to the DLL to get further configurations for the DLL functionality


        // enable the below and add function to get the data
        public string Visible { get; private set; }        
        public int RecordCount { get; private set; }


        public XMLRecord(string sXmlPath)
        {
            Options = new List<Tuple<string, string>>();

            RecordCount = 0;
           
            GetXmlData(sXmlPath);
        }



        // Read the XML file
        // Source: http://stackoverflow.com/questions/5604330/xml-parsing-read-a-simple-xml-file-and-retrieve-values
        private void GetXmlData(string sXmlPath)
        {           
            XDocument doc;
            try
            {
                doc = XDocument.Load(sXmlPath);
            }
            catch (Exception e)
            {
                // Todo: check how to handle a exeption while creating an instance
                MessageBox.Show("XML file contains invalid data." + Environment.NewLine + "Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);               
                return;
            }

            
            foreach (XElement el in doc.Root.Elements()) // TODO: exchange with doc.Root.Elements("program")
            {
                try
                {
                    Executable = el.Element("executable") != null ? el.Element("executable").Value : "";
                    AllArguments = el.Element("staticarg") != null ? el.Element("staticarg").Value : "";
                    Class = el.Element("class") != null ? el.Element("class").Value : "";
                    Description = el.Element("description") != null ? el.Element("description").Value : "";
                    Path = el.Element("path") != null ? el.Element("path").Value : "";                    
                    CommandIsSafe = el.Element("issafe") != null ? el.Element("issafe").Value : "false";
                    UsePipe = el.Element("usepipe") != null ? el.Element("usepipe").Value : "false";
                    Visible = el.Element("visible") != null ? el.Element("visible").Value : "true";
                    IsDll = el.Element("isdll") != null ? el.Element("isdll").Value : "false";
                    ShellExecute = el.Element("ShellExecute") != null ? el.Element("ShellExecute").Value : "false";
                    DllNamespaceName = el.Element("DllNamespaceName") != null ? el.Element("DllNamespaceName").Value : "";
                    DllClassName = el.Element("DllClassName") != null ? el.Element("DllClassName").Value : "";
                    DllMethodName = el.Element("DllMethodName") != null ? el.Element("DllMethodName").Value : "";
                    DllConfigFilePath = el.Element("DllConfigFilePath") != null ? el.Element("DllConfigFilePath").Value : "";
                    ProgramID = el.Attribute("id") != null ? el.Attribute("id").Value : "Invalid <program> element";

                    
                    
                    foreach (XElement elOption in el.Elements("option"))
                    {
                        string option = elOption.Value != null ? elOption.Value : "";
                        string description = elOption.Attribute("desc") != null ? elOption.Attribute("desc").Value : "No description available";                        

                        Options.Add(new Tuple<string, string>(option, description) );
                    }      

                    RecordCount++;

                    // Stop after the first "program" element
                    // TODO: implement logic to get the Nth element
                    break;
                }
                catch
                {
                    // Todo: check how to handle a exeption while creating an instance
                    MessageBox.Show("XML file contains invalid data." + Environment.NewLine + "Failed at <program> element: " + ProgramID, "Error", MessageBoxButton.OK, MessageBoxImage.Error);                    
                    return;
                }

            }

        }
    }
}
