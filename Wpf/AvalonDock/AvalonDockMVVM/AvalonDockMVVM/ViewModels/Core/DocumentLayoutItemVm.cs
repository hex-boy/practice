//#region Copyright

//// --------------------------------------------------------------------------------------------------------------------
//// <copyright company="Siemens AG" file="LayoutItemVm.cs">
////   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
//// </copyright>
//// --------------------------------------------------------------------------------------------------------------------

//#endregion


//#region Used namespaces

//using System;
//using System.Windows.Input;

//#endregion


//namespace AvalonDockMVVM.ViewModels.Core
//{
//    public abstract class DocumentLayoutItemVm : LayoutItemVm
//    {

//        private bool _isClosed;
//        private bool _canClose;


//        #region CONSTRUCTORs

//        protected LayoutItemVm(string title, Uri imageSource, bool isClosed, bool canClose)
//        {
//            Title = title;
//            ImageSource = imageSource;
//            IsClosed = isClosed;
//            CanClose = canClose;
//            CloseCommand = new RelayCommand(call => Close());
//        }

//        #endregion


//        public string Title { get; }

//        public Uri ImageSource { get; }

//        public bool IsClosed
//        {
//            get { return _isClosed; }
//            set
//            {
//                if (_isClosed == value)
//                    return;
//                _isClosed = value;
//                RaisePropertyChanged(nameof(IsClosed));
//            }
//        }

//        public bool CanClose { get; }

//        public ICommand CloseCommand { get; }

//        public void Close()
//        {
//            IsClosed = true;
//        }

//    }
//}