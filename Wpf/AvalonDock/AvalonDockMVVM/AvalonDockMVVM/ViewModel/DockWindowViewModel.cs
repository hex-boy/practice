#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="DockWindowViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Windows.Input;

#endregion


namespace AvalonDockMVVM.ViewModel
{
    public abstract class DockWindowViewModel : BaseViewModel
    {

        #region Properties

        #region CloseCommand

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(call => Close())); }
        }

        #endregion


        #region ImageSource

        private Uri _imageSource;

        public Uri ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource == value)
                    return;
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        #endregion


        #region IsClosed

        private bool _isClosed;

        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                if (_isClosed == value)
                    return;
                _isClosed = value;
                OnPropertyChanged(nameof(IsClosed));
            }
        }

        #endregion


        #region CanClose

        private bool _canClose;

        public bool CanClose
        {
            get { return _canClose; }
            set
            {
                if (_canClose == value)
                    return;
                _canClose = value;
                OnPropertyChanged(nameof(CanClose));
            }
        }

        #endregion


        #region Title

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        #endregion

        #endregion


        public DockWindowViewModel()
        {
            CanClose = true;
            IsClosed = false;
        }

        public void Close()
        {
            IsClosed = true;
        }

    }
}