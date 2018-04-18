using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class LoginModel
    {
        private string inputEmail;
        private string inputPassword;

        // getter and setter
        public string InputEmail
        {
            get { return inputEmail; }
            set { inputEmail = value; }
        }

        public string InputPassword
        {
            get { return inputPassword; }
            set { inputPassword = value; }
        }
           
        public bool LogIn()
        {
            // TO-DO: use socket to login user

            // if success then return true
            return true;

            // else return false
            return false;
        }
    }
}
