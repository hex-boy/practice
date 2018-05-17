#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="SampleAnchorableVm.cs">
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
    public class SampleAnchorableVm : AnchorableLayoutItemVm
    {

        private string _someContentText;

        public SampleAnchorableVm(string title, Uri imageSource)
            : base(title, imageSource) { }

        public string SomeContentText
        {
            get { return _someContentText; }
            set
            {
                _someContentText = value;
                RaisePropertyChanged(nameof(SomeContentText));
            }
        }

    }
}