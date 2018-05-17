#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="SampleHidableAnchorableVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;

using AvalonDockMVVM.ViewModels.Core.Layout;

#endregion


namespace AvalonDockMVVM.ViewModels
{
    public class SampleHidableAnchorableVm : HidableAnchorableLayoutItemVm
    {

        private string _someContentText;

        public SampleHidableAnchorableVm(string title, Uri imageSource)
            : base(title, imageSource) { }

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