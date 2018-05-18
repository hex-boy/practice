#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="HidableAnchorableLayoutItemVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Windows.Input;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels.Core.Layout
{
    public abstract class HidableAnchorableLayoutItemVm : AnchorableLayoutItemVm
    {

        private bool _isVisible;

        protected HidableAnchorableLayoutItemVm(string title, string contentId, Uri imageSource)
            : base(title, contentId, imageSource)
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