#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MainViewModel.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;

using AvalonDockMVVM.ViewModel;

#endregion


namespace AvalonDockMVVM
{
    public class MainViewModel
    {

        public DockManagerVm DockManagerViewModel { get; }

        public MenuViewModel MenuViewModel { get; }

        public MainViewModel()
        {
            var documents = new List<LayoutItemVm>();

            for (var i = 0; i < 6; i++)
                documents.Add(new SampleDocumentVm(
                    "Document 0" + i,
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/document.png"),
                    false,
                    i % 2 == 0) {ContentText = $"Document - 0{i}"});
  
            var anchorables = new List<AnchorableLayoutItemVm>
            {
                new SampleAnchorableVm(
                    "Anchorable 01",
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/property-blue.png"),
                    false,
                    false, false) {SomeContentText = "Anchorable - 01"} ,

                new SampleAnchorableVm(
                    "Anchorable 02",
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/alarm-clock-blue.png"),
                    false,
                    false, false) {SomeContentText = "Anchorable - 02"}
            };

            DockManagerViewModel = new DockManagerVm(documents, anchorables);

            MenuViewModel = new MenuViewModel(documents);
        }

    }
}