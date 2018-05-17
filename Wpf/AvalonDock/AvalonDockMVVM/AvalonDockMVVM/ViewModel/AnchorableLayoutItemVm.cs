#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="AnchorableLayoutItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;

#endregion


namespace AvalonDockMVVM.ViewModel
{
    public class AnchorableLayoutItemVm : LayoutItemVm
    {


        public AnchorableLayoutItemVm(string title, Uri imageSource, bool isClosed, bool canClose, bool canHide)
            : base(title, imageSource, isClosed, canClose)
        {
            CanHide = canHide;
        }


        public bool CanHide { get; }

    }
}