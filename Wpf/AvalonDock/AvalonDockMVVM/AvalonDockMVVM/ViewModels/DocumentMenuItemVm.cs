#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DocumentMenuItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.ComponentModel;

using AvalonDockMVVM.ViewModels.Core;

#endregion


namespace AvalonDockMVVM.ViewModels
{
    public class DocumentMenuItemVm : CheckableMenuItemVm
    {

        private readonly LayoutItemVm _dockWindowVm;


        #region CONSTRUCTORs

        public DocumentMenuItemVm(LayoutItemVm dockWindowVm)
            : base(dockWindowVm.Title, true, null)
        {
            dockWindowVm.PropertyChanged += DockWindowVm_PropertyChanged;
            _dockWindowVm = dockWindowVm;

            UpdateIsChecked();

            PropertyChanged += MenuItemViewModel_PropertyChanged;
        }

        #endregion


        private void MenuItemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IsChecked))
                return;

            UpdateDockWindowIsClosed(IsChecked);
        }

        private void UpdateDockWindowIsClosed(bool itemIsChecked)
        {
            _dockWindowVm.IsClosed = !itemIsChecked;
        }

        private void DockWindowVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutItemVm.IsClosed))
                UpdateIsChecked();
        }

        private void UpdateIsChecked()
        {
            IsChecked = !_dockWindowVm.IsClosed;
        }

    }
}