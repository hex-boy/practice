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

using AvalonDockMVVM.ViewModel;

#endregion


namespace AvalonDockMVVM
{
    public class LayoutItemStyleSelector : StyleSelector
    {

        public Style DocumentStyle { get; set; }

        public Style AnchorableStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is SampleDocumentVm)
                return DocumentStyle;

            if (item is SampleAnchorableVm)
                return AnchorableStyle;

            return base.SelectStyle(item, container);
        }

    }
}