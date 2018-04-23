#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MainWindowVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using Wpf.DynamicNavBar.NavigationBar;
using Wpf.DynamicNavBar.NavigationBar.NavBarTrayVmDts;

#endregion


namespace Wpf.DynamicNavBar
{
    internal class MainWindowVm : VmBase
    {

        public MainWindowVm()
        {
            NavigationBarTray = new NavBarTrayVmDt();
        }

        public NavBarTrayVm NavigationBarTray { get; }

    }
}