using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Edi.Events;
using Edi.Interfaces;
using ICSharpCode.AvalonEdit.Utils;
using Microsoft.Practices.Prism.Commands;


namespace Edi.ViewModel
{


    /// <summary>
    /// Class implements a viewmodel to support the
    /// <seealso cref="Edi.View.Behaviors.AvalonDockLayoutSerializerBehavior"/>
    /// attached behavior which is used to implement
    /// load/save of layout information on application
    /// start and shut-down.
    /// </summary>
    public class AvalonDockLayoutViewModel
    {
        #region fields

        private const string LayoutFileName = "Layout.config";

        private DelegateCommand _loadWorkspaceLayoutFromStringCommand;
        private DelegateCommand _saveWorkspaceLayoutToStringCommand;

        private DelegateCommand _loadLayoutCommand;
        private DelegateCommand<object> _saveLayoutCommand;

        // The XML workspace layout string is stored in this field
        private string _currentLayout;

        readonly ILayoutViewModelParent _parent;

        #endregion fields

        #region constructor

        /// <summary>
        /// Parameterized class constructor to model properties that
        /// are required for access to parent viewmodel.
        /// </summary>
        /// <param name="parent"></param>
        public AvalonDockLayoutViewModel(ILayoutViewModelParent parent) : this()
        {
            _parent = parent;
        }

        /// <summary>
        /// Hidden class constructor
        /// </summary>
        protected AvalonDockLayoutViewModel()
        {
            _currentLayout = null;
        }

        #endregion


        /// <summary>
        /// Get an ICommand to save the WorkspaceLayout
        /// </summary>
        public ICommand SaveWorkspaceLayoutToStringCommand
        {
            get
            {
                // Save layout command can be executed at any time since there always is a layout that can be saved...
                if (_saveWorkspaceLayoutToStringCommand == null)
                {
                    _saveWorkspaceLayoutToStringCommand =
                        new DelegateCommand(SaveWorkspaceLayout,
                            () => _parent.IsBusy == false);

                    CommandManager.RequerySuggested +=
                        (s, e) => _saveWorkspaceLayoutToStringCommand.RaiseCanExecuteChanged();
                }

                return _saveWorkspaceLayoutToStringCommand;
            }
        }

        /// <summary>
        /// Get an ICommand to load the WorkspaceLayout
        /// </summary>
        public ICommand LoadWorkspaceLayoutFromStringCommand
        {
            get
            {
                // Load layout command is not enabled unless the layout string is set.
                if (_loadWorkspaceLayoutFromStringCommand != null)
                    return _loadWorkspaceLayoutFromStringCommand;

                _loadWorkspaceLayoutFromStringCommand = new DelegateCommand(LoadWorkspaceLayout,
                    () =>
                        _parent.IsBusy == false &&
                        string.IsNullOrEmpty(_currentLayout) == false);

                CommandManager.RequerySuggested += (s, e) =>
                    _loadWorkspaceLayoutFromStringCommand.RaiseCanExecuteChanged();

                return _loadWorkspaceLayoutFromStringCommand;
            }
        }

        /// <summary>
        /// Implement a command to load the layout of an AvalonDock-DockingManager instance.
        /// This layout defines the position and shape of each document and tool window
        /// displayed in the application.
        /// 
        /// Parameter:
        /// The command expects a reference to a <seealso cref="DockingManager"/> instance to
        /// work correctly. Not supplying that reference results in not loading a layout (silent return).
        /// </summary>
        public ICommand LoadLayoutCommand
        {
            get
            {
                return _loadLayoutCommand ?? (_loadLayoutCommand = new DelegateCommand(() =>
                {
                    LoadDockingManagerLayout();
                    _parent.IsBusy = false;
                }));
            }
        }

        /// <summary>
        /// Implements a command to save the layout of an AvalonDock-DockingManager instance.
        /// This layout defines the position and shape of each document and tool window
        /// displayed in the application.
        /// 
        /// Parameter:
        /// The command expects a reference to a <seealso cref="string"/> instance to
        /// work correctly. The string is supposed to contain the XML layout persisted
        /// from the DockingManager instance. Not supplying that reference to the string
        /// results in not saving a layout (silent return).
        /// </summary>
        public ICommand SaveLayoutCommand
        {
            get
            {
                return _saveLayoutCommand ?? (_saveLayoutCommand = new DelegateCommand<object>((p) =>
                {
                    var xmlLayout = p as string;

                    if (xmlLayout == null)
                        return;

                    SaveDockingManagerLayout(xmlLayout);
                }));
            }
        }

        #region methods

        /// <summary>
        /// This method is executed if the corresponding
        /// Save Workspace Layout command is executed.
        /// </summary>
        private void SaveWorkspaceLayout()
        {
            var s = new SaveLayoutEventArgs();
            SaveLayoutEvent.Instance.Publish(s);

            _currentLayout = s.XmlLayout;
            SaveDockingManagerLayout(_currentLayout);
        }

        /// <summary>
        /// This method is executed if the corresponding
        /// Load Workspace Layout command is executed.
        /// </summary>
        private void LoadWorkspaceLayout()
        {
            // Is there any layout that could possible be loaded?
            if (string.IsNullOrEmpty(_currentLayout))
                return;

            // Sends a LoadWorkspaceLayout message to registered recipients. The message will reach all recipients
            // that registered for this message type using one of the Register methods.
            // Messenger.Default.Send(new NotificationMessage<string>(current_layout, Notifications.LoadWorkspaceLayout));

            LoadLayoutEvent.Instance.Publish(_currentLayout);
        }

        /// <summary>
        /// Loads the layout of a particular docking manager instance from persistence
        /// and checks whether a file should really be reloaded (some files may no longer
        /// be available).
        /// </summary>
        private void LoadDockingManagerLayout()
        {
            _parent.IsBusy = true;

            var layoutFileName = Path.Combine(_parent.ApplicationUserDirectory, LayoutFileName);

            if (File.Exists(layoutFileName) == false)
            {
                _parent.IsBusy = false;
                return;
            }

            Task.Factory.StartNew(stateObj =>
            {
                string xml;

                // Begin Aysnc Task
                using (var fs = new FileStream(layoutFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = FileReader.OpenStream(fs, Encoding.Default))
                    {
                        xml = reader.ReadToEnd();
                    }
                }

                return xml; // End of async task

            }, null)
                .ContinueWith(ant =>
                {
                    LoadLayoutEvent.Instance.Publish(ant.Result);
                    _parent.IsBusy = false;

                });

        }

        private void SaveDockingManagerLayout(string xmlLayout)
        {
            // Create XML Layout file on close application (for re-load on application re-start)
            if (xmlLayout == null)
                return;

            var fileName = Path.Combine(_parent.ApplicationUserDirectory, LayoutFileName);

            File.WriteAllText(fileName, xmlLayout);
        }

        #endregion methods
    }
}
