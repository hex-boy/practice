#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="SampleDockWindowViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


using System;


namespace AvalonDockMVVM.ViewModel
{
    public class SampleDockWindowViewModel : DockWindowViewModel {

        public SampleDockWindowViewModel(string title, Uri imageSource, bool isClosed, bool canClose)
            : base(title, imageSource, isClosed, canClose) { }

    }
}