#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="LayoutItemTemplateSelector.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System.Windows;
using System.Windows.Controls;

using AvalonDockMvvm.Usage.ViewModels;

#endregion


namespace AvalonDockMvvm.Usage.Views
{
    public class LayoutItemTemplateSelector : DataTemplateSelector
    {

        public DataTemplate DocumentLayoutItemContentTemplate { get; set; }

        public DataTemplate AnchorableLayoutItemContentTemplate { get; set; }

        public DataTemplate HidableAnchorableLayoutItemContentTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SampleDocumentVm)
                return DocumentLayoutItemContentTemplate;

            if (item is SampleAnchorableVm)
                return AnchorableLayoutItemContentTemplate;

            if (item is SampleHidableAnchorableVm)
                return HidableAnchorableLayoutItemContentTemplate;

            return base.SelectTemplate(item, container);
        }

    }
}