#region Copyright

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
    public class HidableAnchorableLayoutItemVm : AnchorableLayoutItemVm
    {

        private bool _isVisible;


        public HidableAnchorableLayoutItemVm(string title, Uri imageSource, bool isClosed, bool canClose)
            : base(title, imageSource, isClosed, canClose)
        {
            HideCommand = new RelayCommand(call => Hide());
            IsVisible = true;
        }


        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand HideCommand { get; }


        private void Hide()
        {
            IsVisible = false;
        }
    }
}