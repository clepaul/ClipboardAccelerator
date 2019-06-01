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
    /// Interaction logic for OptionalArguments.xaml
    /// </summary>
    public partial class OptionalArguments : Window
    {
        // Setup event stuff
        // Source: http://www.codeproject.com/Questions/1031769/Refresh-parent-window-Grid-from-child-window-in-WP
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DataChanged;


        public OptionalArguments(string sXmlBatFile)
        {
            InitializeComponent();            


            XMLRecord xrec = new XMLRecord(sXmlBatFile);
            bool XmlFlag = true;

            // Add the static text
            oaStackPanel.Children.Add(new TextBlock { Text = "Click \"Add\" to populate the Optional Arguments field." + Environment.NewLine, FontSize = 14.667, TextWrapping = TextWrapping.Wrap, FontWeight = FontWeights.Bold });


            foreach (Tuple<string, string> tup in xrec.Options)
            {
                XmlFlag = false;
                if(tup.Item1 != "")
                {
                    DockPanel newPanel = new DockPanel();                 
                    newPanel.Margin = new Thickness(0, 5, 0, 0);
                    newPanel.LastChildFill = true;

                    TextBox tbOptionsString = new TextBox { Text = tup.Item1, FontSize = 14.667, Height = 22, VerticalAlignment = VerticalAlignment.Bottom };
                    TextBlock tBlockInfo = new TextBlock { Text = tup.Item2, FontSize = 14.667, TextWrapping = TextWrapping.Wrap, FontWeight = FontWeights.Bold  };
                   
                    Button bAdd = new Button { Content = "Add", FontSize = 14.667, Height = 22, Tag = tbOptionsString };
                    bAdd.Click += new RoutedEventHandler(bAddOption_Click);
                    DockPanel.SetDock(bAdd, Dock.Right);
                    DockPanel.SetDock(tBlockInfo, Dock.Top);
                    

                    newPanel.Children.Add(tBlockInfo);
                    newPanel.Children.Add(bAdd);
                    newPanel.Children.Add(tbOptionsString);                    

                    

                    oaStackPanel.Children.Add(newPanel);
                    oaStackPanel.Children.Add(new Separator { Visibility = Visibility.Visible, Margin = new Thickness(0, 10, 0, 10) });                    
                }                
            }

            if(XmlFlag)
            {
                DockPanel newPanel = new DockPanel();
                newPanel.Margin = new Thickness(0, 5, 0, 0);
                newPanel.LastChildFill = true;

                TextBlock tBlockNoOptions = new TextBlock { Text = "No options defined in file\n" + sXmlBatFile, FontSize = 14.667, VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Center  };
                
                newPanel.Children.Add(tBlockNoOptions);

                oaStackPanel.Children.Add(newPanel);
            }

            Button bDone = new Button { Content = "Done", FontSize = 14.667, Height = 22 };
            bDone.Click += bDone_Click;
            oaStackPanel.Children.Add(bDone);
        }

        


        private void bAddOption_Click(object sender, RoutedEventArgs e)
        {
            TextBox tbOptionalArgument = ( (sender as Button).Tag as TextBox);
            DataChangedEventHandler handler = DataChanged;

            if (handler != null)
            {
                // Send the data to the parent window using an event               
                handler(this, new OptionalArgumentEventArgs(tbOptionalArgument.Text));
            }
        }

        private void bDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

    public class OptionalArgumentEventArgs : EventArgs
    {
        private readonly string _OptArgString;

        public OptionalArgumentEventArgs(string OptionalArgument)
        {
            _OptArgString = OptionalArgument;
        }

        public string OptArg
        {
            get { return _OptArgString; }
        }
    }

}
