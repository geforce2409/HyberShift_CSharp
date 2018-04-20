using HyberShift_CSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HyberShift_CSharp.Model
{
    public class LoginModel
    {
        private string inputEmail;
        private string inputPassword;

        SocketAPI socket = SocketAPI.GetInstance();

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
            if (InputEmail.Trim().Length == 0)
                return false;

            if (inputPassword.Trim().Length < 6)
                return false;

            return true;
        }

        public void Authentication()
        {

            //test
            Debug.Log("Button login clicked. " + "Email = " + InputEmail + " Password = " + InputPassword);

            //Convert to JSONObject
            JObject userjson = new JObject();
            try
            {
                userjson.Add("email", InputEmail);
                userjson.Add("password", inputPassword);

            }
            catch (JsonException e)
            {
                // TODO Auto-generated catch block
                Debug.Log(e.ToString());
            }

            socket.GetSocket().Emit("authentication", userjson);
        }
    }
}
