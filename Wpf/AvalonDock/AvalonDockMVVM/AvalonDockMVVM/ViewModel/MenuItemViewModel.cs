#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MenuItemViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;
using System.Windows.Input;

#endregion


namespace AvalonDockMVVM.ViewModel
{
    public class MenuItemViewModel : BaseViewModel
    {

        #region Properties

        public string Header { get; set; }

        public bool IsCheckable { get; set; }

        public List<MenuItemViewModel> Items { get; }

        public ICommand Command { get; private set; }


        #region IsChecked

        private bool _IsChecked;

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        #endregion

        #endregion


        public MenuItemViewModel()
        {
            Items = new List<MenuItemViewModel>();
        }

    }
}