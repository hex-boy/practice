using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonDockMVVM.ViewModel
{
    public class SampleAnchorableVm : AnchorableLayoutItemVm
    {

        private string _someContentText;


        public SampleAnchorableVm(string title, Uri imageSource, bool isClosed, bool canClose, bool canHide)
            : base(title, imageSource, isClosed, canClose, canHide) { }


        public string SomeContentText
        {
            get { return _someContentText; }
            set
            {
                _someContentText = value; 
                RaisePropertyChanged(nameof(SomeContentText));
            }
        }

    }
}
