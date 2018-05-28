#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MainWindow.xaml.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces



#endregion


using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using AvalonDockMvvm.Usage.Events;
using AvalonDockMvvm.Usage.ViewModels;
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
            DataContext = new MainWindowVm();

            _avalonDockManager.Loaded += _avalonDockManager_Loaded;

            LoadLayoutEvent.Instance.Subscribe(OnLoadLayout);
            SaveLayoutEvent.Instance.Subscribe(OnSaveLayout);
        }


        private void OnSaveLayout(SaveLayoutEventArgs param)
        {
            string xmlLayoutString;

            using (var fs = new StringWriter())
            {
                var xmlLayout = new XmlLayoutSerializer(_avalonDockManager);

                xmlLayout.Serialize(fs);

                xmlLayoutString = fs.ToString();
            }

            param.XmlLayout = xmlLayoutString;
        }

        /// <summary>
        /// Is executed when PRISM sends a Xml layout string notification
        /// via a sender which could be a viewmodel that wants to receive
        /// the load the <seealso cref="LoadLayoutEvent"/>.
        /// </summary>
        /// <param name="message"></param>
        private void OnLoadLayout(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    var sr = new StringReader(message);
                    var layoutSerializer = new XmlLayoutSerializer(_avalonDockManager);
                    layoutSerializer.LayoutSerializationCallback += UpdateLayout;
                    layoutSerializer.Deserialize(sr);
                }),
                DispatcherPriority.Background);
        }

        private void UpdateLayout(object sender, LayoutSerializationCallbackEventArgs args)
        {
            //var resolver = DataContext as IDocumentViewModelResolver;

            //if (resolver == null)
            //    return;

            //// Get a matching viewmodel for that view via DataContext property of this view
            //var contentViewModel = resolver
            //    .GetContentViewModelFromId(args.Model.ContentId);

            //if (contentViewModel == null)
            //    args.Cancel = true;

            //// found a match - return it
            //args.Content = contentViewModel;
        }

        private async void _avalonDockManager_Loaded(object sender, RoutedEventArgs e)
        {
            //await Task.Run(() => Task.Delay(400));
            //LoadLayout(); // <- Not working yet! (TODO)
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
            LoadLayout();
        }

        private void LoadLayout()
        {
            if (!File.Exists(LAYOUT_XML_FILE_NAME))
                return;
            var layoutSerializer = new XmlLayoutSerializer(_avalonDockManager);
            using (var reader = new StreamReader(LAYOUT_XML_FILE_NAME))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

    }
}