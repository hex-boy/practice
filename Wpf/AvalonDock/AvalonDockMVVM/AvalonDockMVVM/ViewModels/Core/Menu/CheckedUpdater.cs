#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="CheckedUpdater.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.ComponentModel;
using System.Reflection;

#endregion


namespace AvalonDockMVVM.ViewModels.Core.Menu
{
    internal class CheckedUpdater
    {

        private readonly CheckableMenuItemVm _checkableMenuItemVm;
        private readonly INotifyPropertyChanged _layoutItemVm;
        private readonly bool _negated;
        private readonly PropertyInfo _propertyInfo;


        #region CONSTRUCTORs

        public CheckedUpdater(
            CheckableMenuItemVm checkableMenuItemVm, INotifyPropertyChanged layoutItemVm, string boolPropertyName,
            bool negated = false)
        {
            checkableMenuItemVm.PropertyChanged += MenuItemViewModel_PropertyChanged;
            _checkableMenuItemVm = checkableMenuItemVm;

            layoutItemVm.PropertyChanged += DockWindowVm_PropertyChanged;
            _layoutItemVm = layoutItemVm;

            _negated = negated;

            _propertyInfo = _layoutItemVm
                .GetType()
                .GetProperty(boolPropertyName);

            UpdateIsChecked();
        }

        #endregion


        private void MenuItemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(_checkableMenuItemVm.IsChecked))
                return;

            UpdateDockWindowIsClosed(_checkableMenuItemVm.IsChecked);
        }

        private void UpdateDockWindowIsClosed(bool itemIsChecked)
        {
            if (_negated)
                _propertyInfo.SetValue(_layoutItemVm, !itemIsChecked);
            else
                _propertyInfo.SetValue(_layoutItemVm, itemIsChecked);
        }

        private void DockWindowVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _propertyInfo.Name)
                UpdateIsChecked();
        }

        private void UpdateIsChecked()
        {
            _checkableMenuItemVm.IsChecked = _negated ? 
                !(bool) _propertyInfo.GetValue(_layoutItemVm) : 
                (bool)_propertyInfo.GetValue(_layoutItemVm);
        }

    }
}