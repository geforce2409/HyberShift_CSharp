using System;
using System.ComponentModel;
using HyberShift_CSharp.Model;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginModel loginModel;

        // constructor
        public LoginViewModel()
        {
            loginModel = new LoginModel();
            Command = new DelegateCommand(Login);
        }

        // getter and setter
        public string TxtEmail
        {
            get => loginModel.InputEmail;
            set => loginModel.InputEmail = Convert.ToString(value);
        }

        public string FloatingPasswordBox
        {
            get => loginModel.InputPassword;
            set => loginModel.InputPassword = Convert.ToString(value);
        }

        public DelegateCommand Command { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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