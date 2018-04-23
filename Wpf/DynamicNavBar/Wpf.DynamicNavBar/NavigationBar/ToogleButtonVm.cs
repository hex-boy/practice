#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="ToogleButtonVm.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


namespace Wpf.DynamicNavBar.NavigationBar
{
    internal interface IToogleButtonVm : IButtonVm
    {

        bool ToogleButtonOn { get; }

    }


    internal class ToogleButtonVm : ButtonBaseVm, IToogleButtonVm
    {

        private bool _toogleButtonOn;

        public ToogleButtonVm(string name, string description, string imageSourceUri)
            : base(name, description, imageSourceUri) { }


        #region INotifyPropertyChanged

        public bool ToogleButtonOn
        {
            get { return _toogleButtonOn; }
            set
            {
                _toogleButtonOn = value;
                RaisePropertyChanged();
            }
        }

        #endregion

    }
}