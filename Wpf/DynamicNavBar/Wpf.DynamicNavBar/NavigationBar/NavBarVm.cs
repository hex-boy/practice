#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="NavBarVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal class NavBarVm : VmBase
    {

        private bool _toolBarVisible;


        #region CONSTRUCTORs

        public NavBarVm(IEnumerable<IButtonVm> commandsButtons)
        {
            var localCommandButtons = commandsButtons.ToList();
            foreach (var commandButtonVm in localCommandButtons)
            {
                commandButtonVm.PropertyChanged += CommandButtonVmOnPropertyChanged;
            }

            CommandButtonsVms = new ObservableCollection<IButtonVm>(localCommandButtons);

            UpdateVisibility();
        }

        #endregion


        public bool ToolBarVisible
        {
            get { return _toolBarVisible; }
            set
            {
                _toolBarVisible = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IButtonVm> CommandButtonsVms { get; }


        #region PRIVATE METHODS

        private void CommandButtonVmOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            lock (this)
            {
                if (propertyChangedEventArgs.PropertyName == nameof(IButtonVm.IsVisible))
                {
                    UpdateVisibility();
                }
            }
        }

        private void UpdateVisibility()
        {
            ToolBarVisible = CommandButtonsVms.Any(x => x.IsVisible);
        }

        #endregion

    }
}