using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HyberShift_CSharp.Model;
using Prism.Commands;
using HyberShift_CSharp.Utilities;

namespace HyberShift_CSharp.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginModel loginModel;
        private DelegateCommand objCommand;

        // constructor
        public LoginViewModel()
        {
            loginModel = new LoginModel();
            objCommand = new DelegateCommand(this.Login);
        }

        // getter and setter
        public string TxtEmail
        {
            get { return loginModel.InputEmail.ToString(); }
            set { loginModel.InputEmail = Convert.ToString(value); }
        }

        public string FloatingPasswordBox
        {
            get { return loginModel.InputPassword.ToString(); }
            set { loginModel.InputPassword = Convert.ToString(value); }
        }

        public DelegateCommand Command
        {
            get { return objCommand; }
            set { objCommand = value; }
        }

        public void Login()
        {
            loginModel.Authentication();
            if (PropertyChanged != null)
            {
                //PropertyChanged(this, new PropertyChangedEventArgs("attributeX"));  // this will automatically update attributeX
            }
        }

    }
}
