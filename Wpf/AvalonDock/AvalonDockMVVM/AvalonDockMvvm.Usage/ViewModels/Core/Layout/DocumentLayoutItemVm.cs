#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DocumentLayoutItemVm.cs">
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
    public abstract class DocumentLayoutItemVm : LayoutItemVm
    {

        private bool _isClosed;


        #region CONSTRUCTORs

        protected DocumentLayoutItemVm(string title, Uri imageSource, bool isClosed, bool canClose)
            : base(title, imageSource)
        {
            IsClosed = isClosed;
            CanClose = canClose;
            CloseCommand = new RelayCommand(call => Close());
        }

        #endregion


        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                if (_isClosed == value)
                    return;
                _isClosed = value;
                RaisePropertyChanged(nameof(IsClosed));
            }
        }

        public bool CanClose { get; }

        public ICommand CloseCommand { get; }

        public void Close()
        {
            IsClosed = true;
        }

    }
}