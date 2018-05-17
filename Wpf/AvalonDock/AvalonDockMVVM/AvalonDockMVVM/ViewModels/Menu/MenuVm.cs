#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MenuViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;

using AvalonDockMVVM.ViewModels.Core;

#endregion


namespace AvalonDockMVVM.ViewModels.Menu
{
    public class MenuVm
    {

        public IEnumerable<CheckableMenuItemVm> Items { get; }


        #region CONSTRUCTORs

        public MenuVm(IEnumerable<LayoutItemVm> docItems, IEnumerable<HidableAnchorableLayoutItemVm> hidableAnchorableItems)
        {
            var documentsViewMenu = GetDocumentLayoutMenuVm("Documents", docItems);

            var anchorablesViewMenu = GetAnchorableLayoutMenuVm("Anchorables", hidableAnchorableItems);

            var items = new List<CheckableMenuItemVm> { documentsViewMenu, anchorablesViewMenu };
            Items = items;
        }

        #endregion


        private static CheckableMenuItemVm GetDocumentLayoutMenuVm(string menuName, IEnumerable<LayoutItemVm> docItems)
        {
            var documentsViewMenu = new CheckableMenuItemVm(header: menuName, isCheckable: false, command: null);

            foreach (var docItem in docItems)
                documentsViewMenu.Items.Add(new DocumentMenuItemVm(docItem));
            return documentsViewMenu;
        }

        private static CheckableMenuItemVm GetAnchorableLayoutMenuVm(string menuName, IEnumerable<HidableAnchorableLayoutItemVm> docItems)
        {
            var documentsViewMenu = new CheckableMenuItemVm(header: menuName, isCheckable: false, command: null);

            foreach (var docItem in docItems)
                documentsViewMenu.Items.Add(new HidableAnchorableMenuItemVm(docItem));
            return documentsViewMenu;
        }
    }
}