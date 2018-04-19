using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HyberShift_CSharp.Model;  // import namespace of model
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged // this interface is for automatically change property on the view
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginModel obj;
        private DelegateCommand objCommand;

        // constructor
        public LoginViewModel()
        {
            obj = new LoginModel();
            objCommand = new DelegateCommand(obj.Authentication, obj.IsValidLogin);  // delegate to Method1 as Execute() and IsValidAttribute1 as CanExecute()
        }

        // For example, this method is invoked by a button from view
        // and use Method1 of obj (from Model)
        public void Authentication()
        {
            obj.Authentication();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("label1"));  // this will automatically update label1
            }
        }

    }
}
