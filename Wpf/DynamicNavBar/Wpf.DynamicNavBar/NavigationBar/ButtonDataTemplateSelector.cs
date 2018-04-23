#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="ButtonDataTemplateSelector.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Windows;
using System.Windows.Controls;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal class ButtonDataTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var frameworkElement = container as FrameworkElement;
            if (frameworkElement == null)
                throw new ArgumentNullException(nameof(container));

            string templateName;

            if (item is ICommandButtonVm)
                templateName = "CommandButtonVmDataTemplate";
            else
                if (item is IToogleButtonVm)
                    templateName = "ToogleButtonVmDataTemplate";
                else
                    throw new ArgumentOutOfRangeException(nameof(item), @"No data template defined for this view model type");

            return frameworkElement.FindResource(templateName) as DataTemplate;
        }

    }
}