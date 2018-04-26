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
    public class LoginViewModel : BaseViewModel
    public class LoginViewModel : ViewModelBase, IRequireViewIdentification
    {
        private readonly Action<object> navigate;
        private readonly LoginModel loginModel;
        private readonly Guid _viewId;

        // getter and setter
        public DelegateCommand<object> LoginCommand { get; set; }
        public DelegateCommand NavigateCommand { get; set; }

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

        public Guid ViewID
        {
            get { return _viewId; }
        }

        // constructor
        public LoginViewModel()
        {
            loginModel = new LoginModel();
            LoginCommand = new DelegateCommand<object>(Login);
            _viewId = Guid.NewGuid();
        }

        public LoginViewModel(Action<object> navigate):this()
        {
            this.navigate = navigate;
            NavigateCommand = new DelegateCommand(Navigate);
        }

        public void Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                FloatingPasswordBox = ConvertToUnsecureString(secureString);
            }

            if (loginModel.LogIn())
            {
                loginModel.Authentication();
                //CloseWindowManager.CloseLoginWindow(ViewID);
                //TO-DO: Open Main Window
            }
            else
            {
                MessageBoxResult result1 = MessageBox.Show("Email or Password is wrong. Please check again", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //NotifyChanged("attributeX");  // this will automatically update attributeX  
        }

        public void Navigate()
        {
            navigate.Invoke("RegisterViewModel");
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
    }
}