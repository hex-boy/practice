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
    public class CheckableMenuItemVm : BaseVm
    {

        private bool _isChecked;


        #region CONSTRUCTORs

        public CheckableMenuItemVm(string header, bool isCheckable, ICommand command)
        {
            Header = header;
            IsCheckable = isCheckable;
            Command = command;
            Items = new List<CheckableMenuItemVm>();
        }

        #endregion


        public string Header { get; }

        public ICommand Command { get; }

        public bool IsCheckable { get; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked == value)
                    return;

                _isChecked = value;
                RaisePropertyChanged(nameof(IsChecked));
            }
        }

        public List<CheckableMenuItemVm> Items { get; }

    }
}