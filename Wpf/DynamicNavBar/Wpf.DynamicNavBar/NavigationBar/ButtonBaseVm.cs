#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="ButtonBaseVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.ComponentModel;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal interface IButtonVm : INotifyPropertyChanged
    {

        string Name { get; }

        string Description { get; }

        /// <summary>
        /// Gets the image URI source.
        /// </summary>
        /// <remarks>
        /// like i.e. "pack://application:,,,/Images/Menu/Home_5699.png"
        /// </remarks>
        Uri ImageSourceUri { get; }

        bool IsVisible { get; }

    }


    internal abstract class ButtonBaseVm : VmBase, IButtonVm
    {

        protected ButtonBaseVm(string name, string description, string imageSourceUri)
        {
            Name = name;
            Description = description;
            ImageSourceUri = new Uri(imageSourceUri, UriKind.RelativeOrAbsolute);
        }


        #region IButtonVm

        public string Name { get; }

        public string Description { get; }

        /// <summary>
        /// Gets the image URI source.
        /// </summary>
        /// <remarks>
        /// like i.e. "pack://application:,,,/Images/Menu/Home_5699.png"
        /// </remarks>
        public Uri ImageSourceUri { get; }

        public bool IsVisible => true;

        #endregion

    }
}