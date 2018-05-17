#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DockManagerViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

#endregion


namespace AvalonDockMVVM.ViewModel
{
    public class DockManagerVm
    {

        /// <summary>Gets a collection of all visible documents</summary>
        public ObservableCollection<LayoutItemVm> Documents { get; }

        public ObservableCollection<object> Anchorables { get; }

        public DockManagerVm(IEnumerable<LayoutItemVm> dockWindowVms, IEnumerable<AnchorableLayoutItemVm> anchorableVms)
        {
            Documents = new ObservableCollection<LayoutItemVm>();
            Anchorables = new ObservableCollection<object>();

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