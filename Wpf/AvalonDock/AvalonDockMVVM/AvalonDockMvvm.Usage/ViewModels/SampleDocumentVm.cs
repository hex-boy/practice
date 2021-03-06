﻿#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="SampleDocumentVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;

using AvalonDockMvvm.Usage.ViewModels.Core.Layout;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels
{
    public class SampleDocumentVm : DocumentLayoutItemVm
    {

        private string _contentText;

        public SampleDocumentVm(string title, string contentId, Uri imageSource, bool isClosed, bool canClose)
            : base(title, contentId, imageSource, isClosed, canClose) { }

        public string ContentText
        {
            get { return _contentText; }
            set
            {
                _contentText = value;
                RaisePropertyChanged(nameof(ContentText));
            }
        }

    }
}