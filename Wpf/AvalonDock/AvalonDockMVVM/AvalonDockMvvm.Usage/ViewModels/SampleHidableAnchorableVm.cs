#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="SampleHidableAnchorableVm.cs">
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
    public class SampleHidableAnchorableVm : HidableAnchorableLayoutItemVm
    {

        private string _someContentText;

        public SampleHidableAnchorableVm(string title, string contentId, Uri imageSource)
            : base(title, contentId, imageSource) { }

        public string SomeHidableContentText
        {
            get { return _someContentText; }
            set
            {
                _someContentText = value;
                RaisePropertyChanged(nameof(SomeHidableContentText));
            }
        }

    }
}