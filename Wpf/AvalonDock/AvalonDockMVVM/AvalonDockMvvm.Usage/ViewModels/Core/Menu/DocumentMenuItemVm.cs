#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DocumentMenuItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using AvalonDockMvvm.Usage.ViewModels.Core.Layout;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels.Core.Menu
{
    public class DocumentMenuItemVm : CheckableMenuItemVm
    {

        private readonly CheckedUpdater _checkedUpdater;


        #region CONSTRUCTORs

        public DocumentMenuItemVm(DocumentLayoutItemVm docLayoutItemVm)
            : base(docLayoutItemVm.Title, true, null)
        {
            _checkedUpdater = new CheckedUpdater(this, 
                docLayoutItemVm, nameof(DocumentLayoutItemVm.IsClosed), negated: true);
        }

        #endregion

    }
}