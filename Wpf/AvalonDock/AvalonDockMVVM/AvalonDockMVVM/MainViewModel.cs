#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MainViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Collections.Generic;

using AvalonDockMVVM.ViewModel;

#endregion


namespace AvalonDockMVVM
{
    public class MainViewModel
    {

        public DockManagerViewModel DockManagerViewModel { get; }

        public MenuViewModel MenuViewModel { get; }

        public MainViewModel()
        {
            var documents = new List<DockWindowViewModel>();

            for (var i = 0; i < 6; i++)
                documents.Add(new SampleDockWindowViewModel
                {
                    Title = "Sample " + i,
                    CanClose = i % 2 == 0
                });

            DockManagerViewModel = new DockManagerViewModel(documents);
            MenuViewModel = new MenuViewModel(documents);
        }

    }
}