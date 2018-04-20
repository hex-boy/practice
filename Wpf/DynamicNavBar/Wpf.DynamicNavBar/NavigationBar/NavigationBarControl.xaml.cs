#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="NavigationBarControl.xaml.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBarControl : UserControl
    {
        private readonly DispatcherTimer _timer;
        private readonly DispatcherTimer _oldTimer;

        private bool _someCommandCanExecute;
        private bool _oldCommandCanExecute;

        private readonly List<CommandButtonVm> _commandsCollection;


        public NavigationBarControl()
        {
            InitializeComponent();


            //_timer.Start();
        }



    }


}