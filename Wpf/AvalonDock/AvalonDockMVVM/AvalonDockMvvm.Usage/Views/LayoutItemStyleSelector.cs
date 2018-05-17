#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="LayoutItemStyleSelector.cs">
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
    public class LayoutItemStyleSelector : StyleSelector
    {

        public Style DocumentStyle { get; set; }

        public Style AnchorableStyle { get; set; }

        public Style HidableAnchorableStyle { get; set; }


        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is SampleDocumentVm)
                return DocumentStyle;

            if (item is SampleAnchorableVm)
                return AnchorableStyle;

            if (item is SampleHidableAnchorableVm)
                return HidableAnchorableStyle;

            return base.SelectStyle(item, container);
        }

    }
}