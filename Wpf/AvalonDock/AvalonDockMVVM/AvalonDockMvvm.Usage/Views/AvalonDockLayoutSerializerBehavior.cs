using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace AvalonDockMvvm.Usage.Views
{
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