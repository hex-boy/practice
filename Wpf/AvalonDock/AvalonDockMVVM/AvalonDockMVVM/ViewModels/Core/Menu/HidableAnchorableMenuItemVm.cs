#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="HidableAnchorableMenuItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.ComponentModel;

using AvalonDockMVVM.ViewModels.Core.Layout;

#endregion


namespace AvalonDockMVVM.ViewModels.Core.Menu
{
    public class HidableAnchorableMenuItemVm : CheckableMenuItemVm
    {

        private readonly CheckedUpdater _checkedUpdater;


        #region CONSTRUCTORs

        public HidableAnchorableMenuItemVm(HidableAnchorableLayoutItemVm hidableAnchorableLayoutItemVm)
            : base(hidableAnchorableLayoutItemVm.Title, true, null)
        {

            _checkedUpdater = new CheckedUpdater(this,
                hidableAnchorableLayoutItemVm, nameof(HidableAnchorableLayoutItemVm.IsVisible), negated: false);
        }

        #endregion

    }
}