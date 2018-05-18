#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MainWindow.xaml.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces



#endregion


using System.IO;
using System.Windows;

using Xceed.Wpf.AvalonDock.Layout.Serialization;


namespace AvalonDockMvvm.Usage.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string LAYOUT_XML_FILE_NAME = "layout.xml";


        public MainWindow()
        {
            InitializeComponent();
        }


        private void _avalonDockManager_OnLoaded(object sender, RoutedEventArgs e)
        {
            // TODO: here you can reload layout if layout file exist
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var layoutSerializer = new XmlLayoutSerializer(_avalonDockManager);
            using (var writer = new StreamWriter("layout.xml"))
            {
                layoutSerializer.Serialize(writer);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var layoutSerializer = new XmlLayoutSerializer(_avalonDockManager);
            using (var reader = new StreamReader(LAYOUT_XML_FILE_NAME))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

    }
}