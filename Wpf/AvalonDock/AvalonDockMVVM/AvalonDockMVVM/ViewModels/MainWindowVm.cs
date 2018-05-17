#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="MainWindowVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Collections.Generic;

using AvalonDockMVVM.ViewModels.Core;
using AvalonDockMVVM.ViewModels.Menu;

#endregion


namespace AvalonDockMVVM.ViewModels
{
    public class MainWindowVm
    {

        public DockManagerVm DockManagerViewModel { get; }

        public MenuVm MenuViewModel { get; }

        public MainWindowVm()
        {
            var documents = new List<LayoutItemVm>();

            for (var i = 0; i < 6; i++)
                documents.Add(
                    new SampleDocumentVm(
                        "Document 0" + i,
                        new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/document.png"),
                        false,
                        i % 2 == 0) {ContentText = $"Document - 0{i}"});

            var hidableAnchorables = new List<HidableAnchorableLayoutItemVm>
            {
                new SampleHidableAnchorableVm(
                    "Hid Anchorable 01",
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/property-blue.png"),
                    false,
                    false) {SomeHidableContentText = "Hidable Anchorable - 01"},

                new SampleHidableAnchorableVm(
                    "Hid Anchorable 02",
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/property-blue.png"),
                    false,
                    false) {SomeHidableContentText = "Hidable Anchorable - 02"}
            };

            var sampleAnchorableVm = new SampleAnchorableVm(
                "Anchorable 01",
                new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/alarm-clock-blue.png"),
                false,
                false)
            {
                SomeContentText = "Anchorable - 01"
            };

            DockManagerViewModel = new DockManagerVm(documents, new[] { sampleAnchorableVm }, hidableAnchorables);

            MenuViewModel = new MenuVm(documents, hidableAnchorables);
        }

    }
}