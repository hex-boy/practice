#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="NavBarTrayVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal class NavBarTrayVm : ObservableCollection<NavBarVm>
    {

        #region CONSTRUCTORS

        public NavBarTrayVm(IEnumerable<NavBarVm> navigationBars)
            : base(navigationBars) { }

        // For design time data
        public NavBarTrayVm() { }

        #endregion

    }
}