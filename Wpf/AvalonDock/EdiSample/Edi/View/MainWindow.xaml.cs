using System;
using System.Windows.Threading;
using System.IO;
using System.Windows;
using Edi.Events;
using Edi.ViewModel;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Edi.View
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructor

        public MainWindow()
        {
            InitializeComponent();

            DataContext = ApplicationViewModel.This;

            // Register this private methods to receive PRISM event notifications
            LoadLayoutEvent.Instance.Subscribe(OnLoadLayout);
            SaveLayoutEvent.Instance.Subscribe(OnSaveLayout);
        }

        #endregion constructor

        #region Workspace Layout Management

        /// <summary>
        /// Is executed when PRISM sends a <seealso cref="SynchronousEvent"/> notification
        /// that was initiallized by a third party (viewmodel).
        /// </summary>
        /// <param name="param">Can be used to return a result of this event</param>
        private void OnSaveLayout(SaveLayoutEventArgs param)
        {
            string xmlLayoutString;

            using (var fs = new StringWriter())
            {
                var xmlLayout = new XmlLayoutSerializer(DockManager);

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
                    var layoutSerializer = new XmlLayoutSerializer(DockManager);
                    layoutSerializer.LayoutSerializationCallback += UpdateLayout;
                    layoutSerializer.Deserialize(sr);
                }),
                DispatcherPriority.Background);
        }

        /// <summary>
        /// Convert a Avalondock ContentId into a viewmodel instance
        /// that represents a document or tool window. The re-load of
        /// this component is cancelled if the Id cannot be resolved.
        /// 
        /// The result is (viewmodel Id or Cancel) is returned in <paramref name="args"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void UpdateLayout(object sender, LayoutSerializationCallbackEventArgs args)
        {
            var resolver = DataContext as Interfaces.IViewModelResolver;

            if (resolver == null)
                return;

            // Get a matching viewmodel for that view via DataContext property of this view
            var contentViewModel = resolver
                .GetContentViewModelFromId(args.Model.ContentId);

            if (contentViewModel == null)
                args.Cancel = true;

            // found a match - return it
            args.Content = contentViewModel;
        }

        #endregion Workspace Layout Management
    }
}
