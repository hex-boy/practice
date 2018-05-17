#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="SampleDocumentVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;

using AvalonDockMVVM.ViewModels.Core;

#endregion


namespace AvalonDockMVVM.ViewModels
{
    public class SampleDocumentVm : LayoutItemVm
    {

        private string _contentText;

        public SampleDocumentVm(string title, Uri imageSource, bool isClosed, bool canClose)
            : base(title, imageSource, isClosed, canClose) { }

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