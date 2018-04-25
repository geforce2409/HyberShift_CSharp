using System;
using System.ComponentModel;
using HyberShift_CSharp.Model;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly Action<object> navigate;

        private readonly RegisterModel registerModel;

        // getter and setter
        public string TxtEmail
        {
            get => registerModel.Info.Email;
            set => registerModel.Info.Email = Convert.ToString(value);
        }

        public string TxtPassword
        {
            get => registerModel.Info.Password;
            set => registerModel.Info.Password = Convert.ToString(value);
        }

        public string TxtConfirmPassword
        {
            get => registerModel.ConfirmPassword;
            set => registerModel.ConfirmPassword = Convert.ToString(value);
        }

        public string TxtPhone
        {
            get => registerModel.Info.Phone;
            set => registerModel.Info.Phone = Convert.ToString(value);
        }

        public string TxtFullName
        {
            get => registerModel.Info.FullName;
            set => registerModel.Info.FullName = Convert.ToString(value);
        }

        public string ImgAvatarRef
        {
            get => registerModel.Info.AvatarRef;
            set => registerModel.Info.AvatarRef = Convert.ToString(value);
        }

        public DelegateCommand NavigateCommand { get; set; }

        // constructor
        public RegisterViewModel()
        {
            registerModel = new RegisterModel();
            RegisterCommand = new DelegateCommand(Register);
        }

        public RegisterViewModel(Action<object> navigate):this()
        {
            this.navigate = navigate;
            NavigateCommand = new DelegateCommand(Navigate);
        }

        public DelegateCommand RegisterCommand { get; set; }

        public void Register()
        {
            registerModel.PushData();
            //NotifyChanged("attributeX");  // this will automatically update attributeX  
        }

        public void Navigate()
        {
            navigate.Invoke("LoginViewModel");
        }
    }
}