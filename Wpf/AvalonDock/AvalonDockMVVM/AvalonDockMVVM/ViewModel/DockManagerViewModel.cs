#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DockManagerViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

#endregion


namespace AvalonDockMVVM.ViewModel
{
    public class DockManagerViewModel
    {

        /// <summary>Gets a collection of all visible documents</summary>
        public ObservableCollection<DockWindowViewModel> Documents { get; }

        public ObservableCollection<object> Anchorables { get; }

        public DockManagerViewModel(IEnumerable<DockWindowViewModel> dockWindowViewModels)
        {
            Documents = new ObservableCollection<DockWindowViewModel>();
            Anchorables = new ObservableCollection<object>();

            foreach (var document in dockWindowViewModels)
            {
                document.PropertyChanged += DockWindowViewModel_PropertyChanged;
                if (!document.IsClosed)
                    Documents.Add(document);
            }
        }

        private void DockWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var document = sender as DockWindowViewModel;

            if (e.PropertyName == nameof(DockWindowViewModel.IsClosed))
            {
                if (!document.IsClosed)
                    Documents.Add(document);
                else
                    Documents.Remove(document);
            }
        }

    }
}