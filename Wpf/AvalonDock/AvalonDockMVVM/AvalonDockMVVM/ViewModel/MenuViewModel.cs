#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MenuViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;

#endregion


namespace AvalonDockMVVM.ViewModel
{
    public class MenuViewModel
    {

        public IEnumerable<CheckableMenuItemVm> Items { get; }


        public MenuViewModel(IEnumerable<LayoutItemVm> dockWindows)
        {
            var view = new CheckableMenuItemVm (header: "Views", isCheckable:false, command: null);

            foreach (var dockWindow in dockWindows)
                view.Items.Add(new DocumentMenuItemVm(dockWindow));

            var items = new List<CheckableMenuItemVm> {view};
            Items = items;
        }


    }
}