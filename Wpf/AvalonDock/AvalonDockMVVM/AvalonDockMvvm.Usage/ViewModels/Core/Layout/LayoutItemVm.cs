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

        protected LayoutItemVm(string title, string contentId, Uri imageSource)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            if (string.IsNullOrEmpty(contentId))
                throw new ArgumentNullException(nameof(contentId));

            Title = title;
            ContentId = contentId;
            ImageSource = imageSource;
        }

        #endregion


        public string Title { get; }

        public string ContentId { get; }

        public Uri ImageSource { get; }
        
    }
}