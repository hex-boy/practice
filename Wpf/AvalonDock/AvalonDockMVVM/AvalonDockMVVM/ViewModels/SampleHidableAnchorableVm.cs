using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AvalonDockMVVM.ViewModels.Core;


namespace AvalonDockMVVM.ViewModels
{

    public class SampleHidableAnchorableVm : HidableAnchorableLayoutItemVm
    {

        private string _someContentText;


        public SampleHidableAnchorableVm(string title, Uri imageSource, bool isClosed, bool canClose)
            : base(title, imageSource, isClosed, canClose) { }


        public string SomeHidableContentText
        {
            get
            {
                return _someContentText;
            }
            set
            {
                _someContentText = value;
                RaisePropertyChanged(nameof(SomeHidableContentText));
            }
        }

    }
}
