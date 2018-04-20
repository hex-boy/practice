using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

using Wpf.DynamicNavBar.NavigationBar;


namespace Wpf.DynamicNavBar
{
    internal class MainWindowVm : VmBase
    {

        private bool _someCommandCanExecute;
        private bool _oldCommandCanExecute;

        private readonly List<CommandButtonVm> _commandsCollection;


        public MainWindowVm()
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
                new CommandButtonVm(CommandsEnum.Home.ToString(), "Home Command", @"pack://application:,,,/Resources/Icons/Home_5699.png", HomeCommandMethod, HomeCommandCanExecuteMethod),
                new CommandButtonVm(CommandsEnum.Folder.ToString(), "Folder Command", "pack://application:,,,/Resources/Icons/Folder_6221.png", FolderCommandMethod, FolderCommandCanExecuteMethod)
            };

            var navBar1 = new NavBarVm(_commandsCollection);

            var navBar2 = new NavBarVm(
                new List<CommandButtonVm>
                {
                    new CommandButtonVm("Copy", "Execute copy action.", "pack://application:,,,/Resources/Icons/gear_16xLG.png", () =>
                    {
                        var bla = 5;
                    }, () => true)
                });

            var navBarTray = new List<NavBarVm> { navBar1, navBar2 };

            NavigationBarTray = new NavBarTrayVm(navBarTray);

            timer.Start();
        }


        public NavBarTrayVm NavigationBarTray { get; }





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
