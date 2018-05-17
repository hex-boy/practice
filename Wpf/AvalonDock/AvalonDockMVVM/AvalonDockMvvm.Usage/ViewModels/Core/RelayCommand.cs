#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="RelayCommand.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Windows.Input;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels.Core
{
    public class RelayCommand : ICommand
    {

        #region Properties

        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        #endregion


        public RelayCommand(Action<object> execute)
            : this(execute, _ => true) { }

        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            _executeAction = action;
            _canExecuteAction = canExecute;
        }


        #region Methods

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        #endregion

    }
}