using System;
using System.ComponentModel;
using HyberShift_CSharp.Model;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    internal class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly RegisterModel registerModel;

        // constructor
        public RegisterViewModel()
        {
            registerModel = new RegisterModel();
            Command = new DelegateCommand(Register);
        }

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

        public DelegateCommand Command { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Register()
        {
            registerModel.PushData();

            if (PropertyChanged != null)
            {
                //PropertyChanged(this, new PropertyChangedEventArgs("attributeX"));  // this will automatically update attributeX
            }
        }
    }
}