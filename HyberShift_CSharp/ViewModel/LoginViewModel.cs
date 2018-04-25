using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View.SignIn;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginModel loginModel;

        private string signUpVisibile;

        private static LoginViewModel instance;
        // constructor
        public LoginViewModel()
        {
            loginModel = new LoginModel();
            LoginCommand = new DelegateCommand<object>(Login);
            ChangeRegisterViewCommand = new DelegateCommand(ChangeRegisterView);
            signUpVisibile = "collapsed";
        }

        public static LoginViewModel GetInstance()
        {
            if (instance == null)
                instance = new LoginViewModel();
            return instance;
        }

        // getter and setter
        public string SignUpVisibile
        {
            get => signUpVisibile;
            set => signUpVisibile = value;
        }

        public DelegateCommand<object> LoginCommand { get; set; }

        public DelegateCommand ChangeRegisterViewCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get => loginModel.InputEmail;
            set => loginModel.InputEmail = Convert.ToString(value);
        }

        public string FloatingPasswordBox
        {
            get => loginModel.InputPassword;
            set => loginModel.InputPassword = Convert.ToString(value);
        }


        public void Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                FloatingPasswordBox = ConvertToUnsecureString(secureString);
            }

            loginModel.Authentication();
            if (PropertyChanged != null)
            {
                //PropertyChanged(this, new PropertyChangedEventArgs("attributeX"));  // this will automatically update attributeX
            }
        }

        public void ChangeRegisterView()
        {
            RegisterViewModel.GetInstance().SignInVisible = "collapsed";
            SignUpVisibile = "visible";
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("SignInVisible"));
                PropertyChanged(this, new PropertyChangedEventArgs("SignUpVisibile")); 
            }
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}