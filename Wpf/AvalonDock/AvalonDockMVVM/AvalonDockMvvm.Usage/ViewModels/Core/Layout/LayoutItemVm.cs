#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="LayoutItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels.Core.Layout
{
    public abstract class LayoutItemVm : BaseVm
    {

        #region CONSTRUCTORs

        protected LayoutItemVm(string title, Uri imageSource)
        {
            Title = title;
            ImageSource = imageSource;
        }

        #endregion


        public string Title { get; }

        public Uri ImageSource { get; }

    }
}