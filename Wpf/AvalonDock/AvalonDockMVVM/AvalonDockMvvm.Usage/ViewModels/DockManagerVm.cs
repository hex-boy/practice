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

using AvalonDockMvvm.Usage.ViewModels.Core.Layout;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels
{
    public class DockManagerVm
    {

        /// <summary>Gets a collection of all visible documents</summary>
        public ObservableCollection<LayoutItemVm> Documents { get; }

        public ObservableCollection<AnchorableLayoutItemVm> Anchorables { get; }


        #region CONSTRUCTORs

        public DockManagerVm(
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
        }

        #endregion


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
            else
                if (collection.Contains(item))
                    collection.Remove(item);
        }

    }
}