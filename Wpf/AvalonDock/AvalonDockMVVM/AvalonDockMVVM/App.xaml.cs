#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="App.xaml.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Windows;

using AvalonDockMVVM.ViewModels;

#endregion


namespace AvalonDockMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = new Views.MainWindow();
            MainWindow.Show();

            MainWindow.DataContext = new MainWindowVm();
        }

    }
}