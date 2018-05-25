using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Edi.View.Behaviors
{
    /// <summary>
    /// Class implements an attached behavior to load/save a layout for AvalonDock manager.
    /// This layout defines the position and shape of each document and tool window
    /// displayed in the application.
    /// 
    /// Load/Save is triggered through command binding
    /// On application start (AvalonDock.Load event results in LoadLayoutCommand) and
    ///    application shutdown (AvalonDock.Unload event results in SaveLayoutCommand).
    /// 
    /// This implementation of layout save/load is MVVM compliant, robust, and simple to use.
    /// Just add the following code into your XAML:
    /// 
    /// xmlns:AVBehav="clr-namespace:Edi.View.Behavior"
    /// ...
    /// 
    /// avalonDock:DockingManager AnchorablesSource="{Binding Tools}" 
    ///                           DocumentsSource="{Binding Files}"
    ///                           ActiveContent="{Binding ActiveDocument, Mode=TwoWay, Converter={StaticResource ActiveDocumentConverter}}"
    ///                           Grid.Row="3"
    ///                           SnapsToDevicePixels="True"
    ///                AVBehav:AvalonDockLayoutSerializer.LoadLayoutCommand="{Binding LoadLayoutCommand}"
    ///                AVBehav:AvalonDockLayoutSerializer.SaveLayoutCommand="{Binding SaveLayoutCommand}"
    ///                
    /// The LoadLayoutCommand passes a reference of the AvalonDock Manager instance to load the XML layout.
    /// The SaveLayoutCommand passes a string of the XML Layout which can be persisted by the viewmodel/model.
    /// 
    /// Both command bindings work with RoutedCommands or delegate commands (RelayCommand).
    /// </summary>
    public class AvalonDockLayoutSerializerBehavior : Behavior<DockingManager>
    {
        #region DEPENDENCY PROPERTIES

        /// <summary>
        /// Backing store for LoadLayoutCommand dependency property
        /// </summary>
        private static readonly DependencyProperty LoadLayoutCommandProperty =
            DependencyProperty.RegisterAttached("LoadLayoutCommand",
                typeof(ICommand),
                typeof(AvalonDockLayoutSerializerBehavior),
                new PropertyMetadata(null, (o, e) => ((AvalonDockLayoutSerializerBehavior)o).DoDummy(e)));

        /// <summary>
        /// Backing store for SaveLayoutCommand dependency property
        /// </summary>
        private static readonly DependencyProperty SaveLayoutCommandProperty =
            DependencyProperty.RegisterAttached("SaveLayoutCommand",
                typeof(ICommand),
                typeof(AvalonDockLayoutSerializerBehavior),
                new PropertyMetadata(null, (o, e) => ((AvalonDockLayoutSerializerBehavior)o).DoDummy(e)));

        private void DoDummy(DependencyPropertyChangedEventArgs eventArgs)
        {

        }

        #endregion



        public ICommand LoadLayoutCommand
        {
            get { return (ICommand) GetValue(LoadLayoutCommandProperty); }
            set { SetValue(LoadLayoutCommandProperty, value); }
        }

        public ICommand SaveLayoutCommand
        {
            get { return (ICommand) GetValue(SaveLayoutCommandProperty); }
            set { SetValue(SaveLayoutCommandProperty, value); }
        }



        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
            base.OnDetaching();
        }


        #region PRIVATE METHODS

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            Load(AssociatedObject);
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            Save(AssociatedObject);
        }

        /// <summary>
        /// This method is executed when a AvalonDock <seealso cref="DockingManager"/> instance fires the
        /// Load standard (FrameworkElement) event.
        /// </summary>
        /// <param name="dockingManager"></param>
        /// <param name="e"></param>
        private void Load(DockingManager dockingManager)
        {
            // Sanity check just in case this was somehow send by something else
            if (dockingManager == null)
                throw new ArgumentNullException(nameof(dockingManager));

            // There may not be a command bound to this after all
            if (LoadLayoutCommand == null)
                throw new InvalidOperationException("Not properly initialized object");

            // Check whether this attached behaviour is bound to a RoutedCommand
            var command = LoadLayoutCommand as RoutedCommand;
            if (command != null)
            {
                // Execute the routed command
                command.Execute(dockingManager, dockingManager);
            }
            else
            {
                // Execute the Command as bound delegate
                LoadLayoutCommand.Execute(dockingManager);
            }
        }

        /// <summary>
        /// This method is executed when a AvalonDock <seealso cref="DockingManager"/> instance fires the
        /// Unload standard (FrameworkElement) event.
        /// </summary>
        /// <param name="dockingManager"></param>
        /// <param name="e"></param>
        private void Save(DockingManager dockingManager) 
        {
            // Sanity check just in case this was somehow send by something else
            if (dockingManager == null)
                throw new ArgumentNullException(nameof(dockingManager));

            // There may not be a command bound to this after all
            if (SaveLayoutCommand == null)
                throw new InvalidOperationException("Not properly initialized object");

            string xmlLayoutString;

            using (StringWriter fs = new StringWriter())
            {
                var xmlLayout = new XmlLayoutSerializer(dockingManager);

                xmlLayout.Serialize(fs);

                xmlLayoutString = fs.ToString();
            }

            // Check whether this attached behaviour is bound to a RoutedCommand
            var command = SaveLayoutCommand as RoutedCommand;
            if (command != null)
            {
                // Execute the routed command
                command.Execute(xmlLayoutString, dockingManager);
            }
            else
            {
                // Execute the Command as bound delegate
                SaveLayoutCommand.Execute(xmlLayoutString);
            }
        }

        #endregion

    }
}