#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="CommandButtonVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Windows.Input;

using Prism.Commands;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal interface ICommandButtonVm : IButtonVm
    {

        ICommand Command { get; }

        void RaiseCanExecuteChanged();

    }


    internal class CommandButtonVm : ButtonBaseVm, ICommandButtonVm
    {

        private bool _isVisible;


        #region CONSTRUCTORS

        public CommandButtonVm(
            string name, string description, string imageSourceUri,
            Action executeAction, Func<bool> canExecutFunc)
            : base(name, description, imageSourceUri)
        {
            Command = new DelegateCommand(executeAction, canExecutFunc);
            RaiseCanExecuteChanged();
        }

        #endregion


        #region IButtonVm

        public new bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        #region ICommandButtonVm

        public ICommand Command { get; }

        public void RaiseCanExecuteChanged()
        {
            IsVisible = Command.CanExecute(null);

            ((DelegateCommand) Command).RaiseCanExecuteChanged();
        }

        #endregion

    }
}