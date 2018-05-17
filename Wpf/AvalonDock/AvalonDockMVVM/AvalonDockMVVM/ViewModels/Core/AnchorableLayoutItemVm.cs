﻿#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="AnchorableLayoutItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Windows.Input;

#endregion


namespace AvalonDockMVVM.ViewModels.Core
{
    public class AnchorableLayoutItemVm : LayoutItemVm
    {


        public AnchorableLayoutItemVm(string title, Uri imageSource, bool isClosed, bool canClose)
            : base(title, imageSource, isClosed, canClose)
        {

        }

    }
}