﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Utilities;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly Action<object> navigate;
        private readonly RegisterModel registerModel;

        // getter and setter
        public DelegateCommand NavigateCommand { get; set; }

        public string Email
        {
            get => registerModel.Info.Email;
            set => registerModel.Info.Email = Convert.ToString(value);
        }

        public string FloatingPasswordBox
        {
            get => registerModel.Info.Password;
            set => registerModel.Info.Password = Convert.ToString(value);
        }

        public string FloatingConfirmPasswordBox
        {
            get => registerModel.ConfirmPassword;
            set => registerModel.ConfirmPassword = Convert.ToString(value);
        }

        public string Phone
        {
            get => registerModel.Info.Phone;
            set => registerModel.Info.Phone = Convert.ToString(value);
        }

        public string Name
        {
            get => registerModel.Info.FullName;
            set => registerModel.Info.FullName = Convert.ToString(value);
        }

        public string ImgAvatarRef
        {
            get => registerModel.Info.AvatarRef;
            set => registerModel.Info.AvatarRef = Convert.ToString(value);
        }

        // constructor
        public RegisterViewModel()
        {
            registerModel = new RegisterModel();
            RegisterCommand = new DelegateCommand<object>(Register);
        }

        public RegisterViewModel(Action<object> navigate):this()
        {
            this.navigate = navigate;
            NavigateCommand = new DelegateCommand(Navigate);
        }

        public DelegateCommand<object> RegisterCommand { get; set; }

        public void Register(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureStringPassword = passwordContainer.Password;
                var secureStringConfirmPassword = passwordContainer.ConfirmPassword;
                FloatingPasswordBox = ConvertToUnsecureString(secureStringPassword);
                FloatingConfirmPasswordBox = ConvertToUnsecureString(secureStringConfirmPassword);
            }

            registerModel.PushData();
            //NotifyChanged("attributeX");  // this will automatically update attributeX  
        }

        public void Navigate()
        {
            navigate.Invoke("LoginViewModel");
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