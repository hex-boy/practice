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
using System.ComponentModel;
using System.IO;
using System.Linq;

using AvalonDockMvvm.Usage.ViewModels.Core.Layout;
using AvalonDockMvvm.Usage.ViewModels.Core.Menu;

#endregion


namespace AvalonDockMvvm.Usage.ViewModels
{
    public interface ILayoutParentVm
    {

        bool IsBusy { get; set; }

        string ApplicationUserDirectory { get; }

    }


    public interface IDocumentViewModelResolver
    {
        INotifyPropertyChanged GetContentViewModelFromId(string contentId);
    }


    public class MainWindowVm : IDocumentViewModelResolver
    {

        #region CONSTRUCTORs

        public MainWindowVm()
        {
            var documents = GetDocuments();

            var hidableAnchorables = GetHidableAnchorables();

            var notHidableAnchorables = GetNotHidableAnchorables();

            var anchorables = notHidableAnchorables
                .Union(hidableAnchorables);

            DockManagerViewModel = new DockManagerVm(Directory.GetCurrentDirectory(), documents, anchorables);

            MenuViewModel = new MenuVm(documents, hidableAnchorables);
        }

        #endregion


        public DockManagerVm DockManagerViewModel { get; }

        public MenuVm MenuViewModel { get; }


        #region PRIVATE METHODS

        private static AnchorableLayoutItemVm[] GetNotHidableAnchorables()
        {
            var sampleAnchorableVm = new SampleAnchorableVm(
                "Anchorable 01",
                "Anchorable01",
                new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/alarm-clock-blue.png"))
            {
                SomeContentText = "Anchorable - 01"
            };

            var notHidableAnchorables = new AnchorableLayoutItemVm[] {sampleAnchorableVm};
            return notHidableAnchorables;
        }

        private static List<HidableAnchorableLayoutItemVm> GetHidableAnchorables()
        {
            var hidableAnchorableSamples = new List<HidableAnchorableLayoutItemVm>
            {
                new SampleHidableAnchorableVm(
                    "Hid Anchorable 01", "HidAnchorable01",
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/property-blue.png"))
                {
                    SomeHidableContentText = "Hidable Anchorable - 01"
                },

                new SampleHidableAnchorableVm(
                    "Hid Anchorable 02", "HidAnchorable02",
                    new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/property-blue.png"))
                {
                    SomeHidableContentText = "Hidable Anchorable - 02"
                }
            };
            return hidableAnchorableSamples;
        }

        private static List<DocumentLayoutItemVm> GetDocuments()
        {
            var documents = new List<DocumentLayoutItemVm>();

            for (var i = 0; i < 6; i++)
                documents.Add(
                    new SampleDocumentVm(
                        "Document 0" + i,
                        "Document0" + i,
                        new Uri(@"pack://application:,,,/AvalonDockMVVM;component/Images/document.png"),
                        false,
                        i % 2 == 0) {ContentText = $"Document - 0{i}"});
            return documents;
        }

        #endregion

        #region IDocumentViewModelResolver

        // Es wird verwendet um z.B. Dokument-View Models zu laden
        public INotifyPropertyChanged GetContentViewModelFromId(string contentId)
        {
            return null;
        }

        #endregion
    }
}