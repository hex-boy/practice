﻿using System.ComponentModel;


namespace AvalonDockMVVM.ViewModel {
    public class DocumentMenuItemViewModel : CheckableMenuItemVm
    {

        private readonly DockWindowViewModel _dockWindowVm;


        #region CONSTRUCTORs

        public DocumentMenuItemViewModel(DockWindowViewModel dockWindowVm)
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
            if (e.PropertyName == nameof(DockWindowViewModel.IsClosed))
                UpdateIsChecked();
        }

        private void UpdateIsChecked()
        {
            IsChecked = !_dockWindowVm.IsClosed;
        }

    }
}