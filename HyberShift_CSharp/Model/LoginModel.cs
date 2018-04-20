using HyberShift_CSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class LoginModel
    {
        private string inputEmail;
        private string inputPassword;

        Socket socket = SocketAPI.GetInstance().GetSocket();

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

            //Convert to JSONObject
            JObject userjson = new JObject();
            try
            {
                userjson.Add("email", InputEmail);
                userjson.Add("password", inputPassword);

            }
            catch (JsonException e)
            {
                Debug.Log(e.ToString());
            }

            socket.Emit("authentication", userjson);         

            // [SAMPLE] Method for receiving event from socket server
            HandleOnSocketEvent();
        }

        //[SAMPLE] Method handle "On" event from socket server
        public void HandleOnSocketEvent()
        {
            socket.On("<event_name_1>", () => {
                Debug.Log("Received response 1 of socket");

                // NOTICE: SOME EVENT NEED TO BE RUNNED ON ANOTHER THREAD
                // Start a new thread in java (old project):
            //    Platform.runLater(new Runnable(){
            //                @Override

            //                public void run()
            //                {
            //                    ...
            //                }
            //    });

                // In C# ???
                
            });

            socket.On("<event_name_2>", () => {
                Debug.Log("Received response 2 of socket");
            });
        }
    }
}
