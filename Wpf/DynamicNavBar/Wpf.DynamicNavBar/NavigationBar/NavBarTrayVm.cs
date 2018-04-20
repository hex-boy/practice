#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="NavBarTrayControlVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal class NavBarTrayVm : ObservableCollection<NavBarVm>
    {

        #region CONSTRUCTORS

        public NavBarTrayVm(IEnumerable<NavBarVm> navigationBars)
            : base(navigationBars)
        {
            
        }

        // For design time data
        public NavBarTrayVm()
        {
            var navBar2 = new NavBarVm(
                new List<CommandButtonVm>
                {
                    new CommandButtonVm("Copy", "Execute copy action.", "pack://application:,,,/Resources/Icons/gear_16xLG.png.png", () => { }, () => true)
                });

            Add(navBar2);
        }

        #endregion

    }
}