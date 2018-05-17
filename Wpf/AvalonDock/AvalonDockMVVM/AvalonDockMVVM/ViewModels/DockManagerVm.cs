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


        #region CONSTRUCTORs

        public DockManagerVm(IEnumerable<LayoutItemVm> dockWindowVms, IEnumerable<AnchorableLayoutItemVm> anchorableVms, IEnumerable<HidableAnchorableLayoutItemVm> hidableAnchorableVms)
        {
            Documents = new ObservableCollection<LayoutItemVm>();
            Anchorables = new ObservableCollection<AnchorableLayoutItemVm>();

            foreach (var document in dockWindowVms)
            {
                document.PropertyChanged += DocumentLayoutItemVm_PropertyChanged;
                AddRemoveLayoutItemVm(Documents, document, document.IsClosed);
            }

            foreach (var anchorableVm in anchorableVms)
            {
                AddRemoveLayoutItemVm(Anchorables, anchorableVm, false);
            }

            foreach (var anchorableVm in hidableAnchorableVms)
            {
                AddRemoveLayoutItemVm(Anchorables, anchorableVm, false);
            }
        }

        #endregion


        private void DocumentLayoutItemVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var senderLocal = sender as LayoutItemVm;

            if (e.PropertyName == nameof(LayoutItemVm.IsClosed))
            {
                AddRemoveLayoutItemVm(Documents, senderLocal, senderLocal.IsClosed);
            }
        }

        private void AddRemoveLayoutItemVm<T>(ObservableCollection<T> collection, T item, bool doRemove)
            where T : LayoutItemVm
        {
            if (!doRemove)
                collection.Add(item);
            else
                if (collection.Contains(item))
                    collection.Remove(item);
        }

    }
}