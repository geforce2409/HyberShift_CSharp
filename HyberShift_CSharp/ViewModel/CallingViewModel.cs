using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    public class CallingViewModel: BaseViewModel
    {
        private object selectedViewModel;

        public CallingViewModel()
        {
            SelectedViewModel = new WaitingCallViewModel(ViewModelNavigator);
        }

        public object SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                NotifyChanged("SelectedViewModel");
            }
        }

        public void ViewModelNavigator(object obj)
        {
            if (obj.ToString() == "WaitingCallViewModel") SelectedViewModel = new WaitingCallViewModel(ViewModelNavigator);
            if (obj.ToString() == "OnGoingCallViewModel") SelectedViewModel = new OnGoingCallViewModel(ViewModelNavigator);
        }
    }
}
