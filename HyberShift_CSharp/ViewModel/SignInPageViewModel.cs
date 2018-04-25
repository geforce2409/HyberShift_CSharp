using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    public class SignInPageViewModel: ViewModelBase
    {
        public LoginViewModel LoginVM { get; set; }
        public RegisterViewModel RegisterVM { get; set; }

        private object selectedViewModel;

        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                NotifyChanged("SelectedViewModel");
            }
        }

        public SignInPageViewModel()
        {
            SelectedViewModel = new LoginViewModel(ViewModelNavigator);
            //SelectedViewModel = new RegisterViewModel(RegisterVMNavigator);
        }

        public void ViewModelNavigator(object obj)
        {
            if (obj.ToString() == "RegisterViewModel")
            {
                SelectedViewModel = new RegisterViewModel(ViewModelNavigator);
            }
            if (obj.ToString() == "LoginViewModel")
            {
                SelectedViewModel = new LoginViewModel(ViewModelNavigator);
            }
        }

    }
}
