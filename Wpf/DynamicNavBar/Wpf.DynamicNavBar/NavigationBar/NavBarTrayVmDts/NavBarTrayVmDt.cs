#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="NavBarTrayVmDt.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar.NavBarTrayVmDts
{
    internal class NavBarTrayVmDt : NavBarTrayVm
    {

        private bool _someCommandCanExecute;
        private bool _oldCommandCanExecute;

        private readonly List<CommandButtonVm> _commandsCollection;


        #region CONSTRUCTORS

        // For design time data
        public NavBarTrayVmDt()
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher)
            {
                Interval = TimeSpan.FromSeconds(5),
                IsEnabled = true
            };
            var oldTimer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher)
            {
                Interval = TimeSpan.FromSeconds(3),
                IsEnabled = true
            };

            timer.Tick += TimerOnTick;
            oldTimer.Tick += OldTimerOnTick;

            _commandsCollection = new List<CommandButtonVm>
            {
                new CommandButtonVm(
                    CommandsEnum.Home.ToString(), "Home Command", @"pack://application:,,,/Resources/Icons/Home_5699.png", HomeCommandMethod,
                    HomeCommandCanExecuteMethod),
                new CommandButtonVm(
                    CommandsEnum.Folder.ToString(), "Folder Command", "pack://application:,,,/Resources/Icons/Folder_6221.png", FolderCommandMethod,
                    FolderCommandCanExecuteMethod)
            };

            var navBar1 = new NavBarVm(_commandsCollection);

            Add(navBar1);

            var navBar2 = new NavBarVm(
                new List<IButtonVm>
                {
                    new CommandButtonVm(
                        "Copy", "Execute copy action.", "pack://application:,,,/Resources/Icons/GenerateAll.png",
                        () => { MessageBox.Show("Copy Command - executed"); }, () => true),
                    new ToogleButtonVm("Store On/Off", "Store to database is activated/deactivated", "pack://application:,,,/Resources/Icons/settings_16.png")
                });

            Add(navBar2);

            timer.Start();
        }

        #endregion


        private void FolderCommandMethod()
        {
            MessageBox.Show("Folder Command - executed");
        }

        private bool FolderCommandCanExecuteMethod()
        {
            return _oldCommandCanExecute;
        }

        private bool HomeCommandCanExecuteMethod()
        {
            return _someCommandCanExecute;
        }

        private void HomeCommandMethod()
        {
            MessageBox.Show("Home Command - executed");
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            _someCommandCanExecute = !_someCommandCanExecute;

            _commandsCollection
                .First(x => x.Name == CommandsEnum.Home.ToString())
                .RaiseCanExecuteChanged();
        }

        private void OldTimerOnTick(object sender, EventArgs e)
        {
            _oldCommandCanExecute = !_oldCommandCanExecute;

            _commandsCollection
                .First(x => x.Name == CommandsEnum.Folder.ToString())
                .RaiseCanExecuteChanged();
        }

    }


    internal enum CommandsEnum
    {

        Home,
        Folder

    }
}