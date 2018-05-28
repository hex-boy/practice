#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DockManagerVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using AvalonDockMvvm.Usage.Events;
using AvalonDockMvvm.Usage.ViewModels.Core;
using AvalonDockMvvm.Usage.ViewModels.Core.Layout;
using Prism.Commands;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels
{
    public class DockManagerVm : BaseVm
    {
        private const string LAYOUT_FILE_NAME = "layout.xml";

        private readonly string _layoutFilePath;
        private bool _isBusy;
        

        #region CONSTRUCTORs

        public DockManagerVm(
            string applicationDirectory,
            IEnumerable<DocumentLayoutItemVm> docLayoutVms,
            IEnumerable<AnchorableLayoutItemVm> anchorableVms)
        {
            Documents = new ObservableCollection<LayoutItemVm>();
            foreach (var document in docLayoutVms)
            {
                document.PropertyChanged += DocumentLayoutItemVm_PropertyChanged;
                AddRemoveLayoutItemVm(Documents, document, document.IsClosed);
            }

            Anchorables = new ObservableCollection<AnchorableLayoutItemVm>();
            foreach (var anchorableLayoutItemVm in anchorableVms)
            {
                Anchorables.Add(anchorableLayoutItemVm);
            }

            LoadLayoutCommand = new DelegateCommand(async () =>
            {
                IsBusy = true;
                await LoadDockingManagerLayout();
                IsBusy = false;
            });

            SaveLayoutCommand = new DelegateCommand<string>(async (fileTextContent) =>
            {
                IsBusy = true;
                await SaveDockingManagerLayout(fileTextContent);
                IsBusy = false;
            });

            _layoutFilePath = Path.Combine(applicationDirectory, LAYOUT_FILE_NAME);
        }

        #endregion


        /// <summary>Gets a collection of all visible documents</summary>
        public ObservableCollection<LayoutItemVm> Documents { get; }

        public ObservableCollection<AnchorableLayoutItemVm> Anchorables { get; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));

                Application.Current.Dispatcher.BeginInvoke(
                    new Action(() => Mouse.OverrideCursor = value == false ? null : Cursors.Wait),
                    DispatcherPriority.Background);
            }
        }

        public ICommand LoadLayoutCommand { get; }

        public ICommand SaveLayoutCommand { get; }


        private void DocumentLayoutItemVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var senderLocal = sender as DocumentLayoutItemVm;

            if (e.PropertyName == nameof(DocumentLayoutItemVm.IsClosed))
            {
                AddRemoveLayoutItemVm(Documents, senderLocal, senderLocal.IsClosed);
            }
        }

        private void AddRemoveLayoutItemVm<T>(ObservableCollection<T> collection, T item, bool doRemove)
            where T : LayoutItemVm
        {
            if (!doRemove)
                collection.Add(item);
            else if (collection.Contains(item))
                collection.Remove(item);
        }

        private async Task LoadDockingManagerLayout()
        {
            if (File.Exists(_layoutFilePath) == false)
            {
                return;
            }

            await Task.Run(() => File.ReadAllText(_layoutFilePath))
                .ContinueWith(task => LoadLayoutEvent.Instance.Publish(task.Result));
        }

        private async Task SaveDockingManagerLayout(string xmlLayout)
        {
            if (xmlLayout == null)
                return;

            await Task.Run(() => File.WriteAllText(_layoutFilePath, xmlLayout));
        }
    }
}