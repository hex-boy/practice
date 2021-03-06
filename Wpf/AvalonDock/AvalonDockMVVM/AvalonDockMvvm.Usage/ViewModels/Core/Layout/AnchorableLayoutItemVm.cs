﻿#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="AnchorableLayoutItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels.Core.Layout
{
    public abstract class AnchorableLayoutItemVm : LayoutItemVm
    {

        protected AnchorableLayoutItemVm(string title, string contentId, Uri imageSource)
            : base(title, contentId, imageSource) { }

    }
}