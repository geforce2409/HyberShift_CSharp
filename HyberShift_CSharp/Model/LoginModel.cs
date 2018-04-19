using HyberShift_CSharp.Utilities;
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

        // constructor
        public LoginModel()
        {
            inputEmail = "";
            inputPassword = "";
        }

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
            if (IsValidLogin())
            {
                Authentication();
                return true;
            }

            // else return false
            else
            {
                return false;
            }
        }

        public bool IsValidLogin()
        {
            //if (InputEmail.Trim().Length == 0)
            //    return false;

            //if (inputPassword.Trim().Length == 0)
            //    return false;

            return true;
        }

        public void Authentication()
        {

            //test
            Debug.Log("Button login clicked. " + "Email = " + InputEmail + " Password = " + InputPassword);

            ////Convert to JSONObject
            //JSONObject userjson = new JSONObject();
            //try
            //{
            //    userjson.put("email", InputEmail);
            //    userjson.put("password", inputPassword);

            //}
            //catch (JSONException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}

            //socket.emit("authentication", userjson);
        }
    }
}
