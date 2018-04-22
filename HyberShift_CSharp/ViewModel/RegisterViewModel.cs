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
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RegisterModel registerModel;
        private DelegateCommand objCommand;

        // constructor
        public RegisterViewModel()  
        {
            registerModel = new RegisterModel();
            objCommand = new DelegateCommand(this.Register);
        }

        // getter and setter
        public string TxtEmail
        {
            get { return registerModel.Info.Email.ToString(); }
            set { registerModel.Info.Email = Convert.ToString(value); }
        }

        public string TxtPassword
        {
            get { return registerModel.Info.Password.ToString(); }
            set { registerModel.Info.Password = Convert.ToString(value); }
        }

        public string TxtConfirmPassword
        {
            get { return registerModel.ConfirmPassword.ToString(); }
            set { registerModel.ConfirmPassword = Convert.ToString(value); }
        }

        public string TxtPhone
        {
            get { return registerModel.Info.Phone.ToString(); }
            set { registerModel.Info.Phone = Convert.ToString(value); }
        }

        public string TxtFullName
        {
            get { return registerModel.Info.FullName.ToString(); }
            set { registerModel.Info.FullName = Convert.ToString(value); }
        }

        public string ImgAvatarRef
        {
            get { return registerModel.Info.AvatarRef.ToString(); }
            set { registerModel.Info.AvatarRef = Convert.ToString(value); }
        }

        public DelegateCommand Command
        {
            get { return objCommand; }
            set { objCommand = value; }
        }

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
