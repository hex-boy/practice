#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="NavigationBarVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal class NavBarVm : VmBase
    {

        private bool _toolBarVisible;
        


        #region CONSTRUCTORs

        public NavBarVm(IEnumerable<CommandButtonVm> commandsButtons)
        {
            var localCommandButtons = commandsButtons.ToList();
            foreach (var commandButtonVm in localCommandButtons)
            {
                commandButtonVm.CanExecuteChanged += CommandButtonVmOnCanExecuteChanged;
            }

            CommandButtonsVms = new ObservableCollection<CommandButtonVm>(localCommandButtons);

            UpdateToolBarVisibility();
        }

        private void CommandButtonVmOnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            UpdateToolBarVisibility();
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

        public ObservableCollection<CommandButtonVm> CommandButtonsVms { get; }





        private void UpdateToolBarVisibility()
        {
            lock (this)
            {
                ToolBarVisible = CommandButtonsVms.Any(x => x.CanExecute());
            }
        }
    }

}