#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="CommandButtonVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Prism.Commands;

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal class CommandButtonVm : DelegateCommand, INotifyPropertyChanged
    {


        #region CONSTRUCTORS

        public CommandButtonVm(
            string name, string description, string imageSourceUri,
            Action executeAction, Func<bool> canExecutFunc)
            : base(executeAction, canExecutFunc)
        {
            Name = name;
            Description = description;
            ImageSourceUri = new Uri(imageSourceUri, UriKind.RelativeOrAbsolute);
        }

        #endregion


        public string Name { get; }

        public string Description { get; }


        /// <summary>
        /// Gets the image URI source.
        /// </summary>
        /// <remarks>
        /// like i.e. "pack://application:,,,/Images/Menu/Home_5699.png"
        /// </remarks>
        public Uri ImageSourceUri { get; }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

}