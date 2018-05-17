#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DockManagerVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using AvalonDockMVVM.ViewModels.Core;

#endregion


namespace AvalonDockMVVM.ViewModels
{
    public class DockManagerVm
    {

        /// <summary>Gets a collection of all visible documents</summary>
        public ObservableCollection<LayoutItemVm> Documents { get; }

        public ObservableCollection<AnchorableLayoutItemVm> Anchorables { get; }

        public DockManagerVm(IEnumerable<LayoutItemVm> dockWindowVms, IEnumerable<AnchorableLayoutItemVm> anchorableVms)
        {
            Documents = new ObservableCollection<LayoutItemVm>();
            Anchorables = new ObservableCollection<AnchorableLayoutItemVm>();

            foreach (var document in dockWindowVms)
            {
                document.PropertyChanged += DockWindowViewModel_PropertyChanged;
                if (!document.IsClosed)
                    Documents.Add(document);
            }

            foreach (var anchorableLayoutItemVm in anchorableVms)
            {
                Anchorables.Add(anchorableLayoutItemVm);
            }
        }

        private void DockWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var document = sender as LayoutItemVm;

            if (e.PropertyName == nameof(LayoutItemVm.IsClosed))
            {
                if (!document.IsClosed)
                    Documents.Add(document);
                else
                    Documents.Remove(document);
            }
        }

    }
}