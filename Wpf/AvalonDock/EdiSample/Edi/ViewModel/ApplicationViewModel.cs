using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Edi.Interfaces;
using Edi.ViewModel.Base;
using ICSharpCode.AvalonEdit.Document;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Win32;


namespace Edi.ViewModel
{


    public enum ToggleEditorOption
    {
        WordWrap = 0,
        ShowLineNumber = 1,
        ShowEndOfLine = 2,
        ShowSpaces = 3,
        ShowTabs = 4
    }

    /// <summary>
    /// This class manages the viewmodel properties and methods for the entire application.
    /// </summary>
    public class ApplicationViewModel : NotificationObject, IViewModelResolver, ILayoutViewModelParent
    {
        #region fields

        private bool _isBusy = true;

        readonly ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();
        ReadOnlyObservableCollection<FileViewModel> _readonyFiles;
        ToolViewModel[] _tools;
        FileStatsViewModel _fileStats;

        #endregion fields


        #region constructor

        /// <summary>
        /// Class constructor
        /// </summary>
        protected ApplicationViewModel()
        {
            ADLayout = new AvalonDockLayoutViewModel(this);
        }

        #endregion constructor



        public static ApplicationViewModel This { get; } = new ApplicationViewModel();

        /// <summary>
        /// List all file documents managed by this viewmodel.
        /// </summary>
        public ReadOnlyObservableCollection<FileViewModel> Files => _readonyFiles ?? (_readonyFiles = new ReadOnlyObservableCollection<FileViewModel>(_files));

        /// <summary>
        /// List all tool windows managed by this viewmodel.
        /// </summary>
        public IEnumerable<ToolViewModel> Tools => _tools ?? (_tools = new ToolViewModel[] {FileStats});

        public FileStatsViewModel FileStats => _fileStats ?? (_fileStats = new FileStatsViewModel());

        /// <summary>
        /// Get/set property to signal whether application is busy or not.
        /// 
        /// The property set method invokes a CommandManager.InvalidateRequerySuggested()
        /// if the new value is different from the old one. The mouse cursor is set to Wait
        /// if the application is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }

            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;

                RaisePropertyChanged(() => IsBusy);

                CommandManager.InvalidateRequerySuggested();

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Mouse.OverrideCursor = value == false ? 
                            null :
                            Cursors.Wait;
                    }),
                    DispatcherPriority.Background);
            }
        }

        private FileViewModel _activeDocument;

        public FileViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument == value) return;
                _activeDocument = value;

                RaisePropertyChanged(nameof(ActiveDocument));
                ActiveDocumentChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ActiveDocumentChanged;


        /// <summary>
        /// Get a path to the directory where the application
        /// can persist/load user data on session exit and re-start.
        /// </summary>
        public string ApplicationUserDirectory
        {
            get
            {
                var dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                 Path.DirectorySeparatorChar + Company;
                if (Directory.Exists(dirPath) == false)
                    Directory.CreateDirectory(dirPath);
                return dirPath;
            }
        }

        public string Company => "EdiDemo";


        DelegateCommand<object> _openCommand;

        public ICommand OpenCommand => _openCommand ??
                                       (_openCommand = new DelegateCommand<object>(OnOpen, CanOpen));

        private bool CanOpen(object parameter)
        {
            return true;
        }

        private void OnOpen(object parameter)
        {
            var dlg = new OpenFileDialog();
            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            var fileViewModel = Open(dlg.FileName);
            ActiveDocument = fileViewModel;
        }

        public FileViewModel Open(string filepath)
        {
            var fileViewModel = _files.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;

            if (File.Exists(filepath) == false)
                return null;

            fileViewModel = new FileViewModel(filepath);
            _files.Add(fileViewModel);

            return fileViewModel;
        }


        DelegateCommand<object> _newCommand;

        public ICommand NewCommand => _newCommand ?? (_newCommand = new DelegateCommand<object>(OnNew, CanNew));

        private bool CanNew(object parameter)
        {
            return true;
        }

        private void OnNew(object parameter)
        {
            _files.Add(new FileViewModel() {Document = new TextDocument()});
            ActiveDocument = _files.Last();
        }

        /// <summary>
        /// Expose command to load/save AvalonDock layout on application startup and shut-down.
        /// </summary>
        public AvalonDockLayoutViewModel ADLayout { get; }


        #region close save file handling methods

        internal void Close(FileViewModel fileToClose)
        {
            if (fileToClose.IsDirty)
            {
                var res = MessageBox.Show($"Save changes for file '{fileToClose.FileName}'?",
                    "AvalonDock Test App", MessageBoxButton.YesNoCancel);
                switch (res)
                {
                    case MessageBoxResult.Cancel:
                        return;
                    case MessageBoxResult.Yes:
                        Save(fileToClose);
                        break;
                    case MessageBoxResult.None:
                        break;
                    case MessageBoxResult.OK:
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _files.Remove(fileToClose);
        }

        internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
        {
            if (fileToSave.FilePath == null || saveAsFlag)
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog().GetValueOrDefault())
                    fileToSave.FilePath = dlg.SafeFileName;
            }

            File.WriteAllText(fileToSave.FilePath, fileToSave.Document.Text);
            ActiveDocument.IsDirty = false;
        }

        #endregion close save file handling methods

        #region ToggleEditorOptionCommand

        DelegateCommand<object> _toggleEditorOptionCommand;

        public ICommand ToggleEditorOptionCommand => _toggleEditorOptionCommand ?? (_toggleEditorOptionCommand = new DelegateCommand<object>(
                                                         OnToggleEditorOption,
                                                         CanToggleEditorOption));

        private bool CanToggleEditorOption(object parameter)
        {
            return ActiveDocument != null;
        }

        private void OnToggleEditorOption(object parameter)
        {
            var f = ActiveDocument;

            if (parameter == null)
                return;

            if ((parameter is ToggleEditorOption) == false)
                return;

            var t = (ToggleEditorOption) parameter;

            if (f == null)
                return;

            switch (t)
            {
                case ToggleEditorOption.WordWrap:
                    f.WordWrap = !f.WordWrap;
                    break;

                case ToggleEditorOption.ShowLineNumber:
                    f.ShowLineNumbers = !f.ShowLineNumbers;
                    break;

                case ToggleEditorOption.ShowSpaces:
                    f.TextOptions.ShowSpaces = !f.TextOptions.ShowSpaces;
                    break;

                case ToggleEditorOption.ShowTabs:
                    f.TextOptions.ShowTabs = !f.TextOptions.ShowTabs;
                    break;

                case ToggleEditorOption.ShowEndOfLine:
                    f.TextOptions.ShowEndOfLine = !f.TextOptions.ShowEndOfLine;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion ToggleEditorOptionCommand


        /// <summary>
        /// Translate a string ID into a matching viewmodel instance (tool window or document)
        /// that is managed by this class.
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns>viewmodel instance or null (if match was not succesful)</returns>
        public INotifyPropertyChanged GetContentViewModelFromId(string contentId)
        {
            // Query for a tool window and return it
            var anchorableVm = Tools.FirstOrDefault(d => d.ContentId == contentId);
            if (anchorableVm != null)
                return anchorableVm;

            // Query for a matching document and return it
            return Open(contentId);
        }

    }
}
