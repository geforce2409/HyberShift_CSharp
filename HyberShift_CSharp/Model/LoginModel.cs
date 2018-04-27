﻿using HyberShift_CSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class LoginModel
    {
        private readonly Socket socket;

        // constructor
        public LoginModel()
        {
            InputEmail = "";
            InputPassword = "";
            socket = SocketAPI.GetInstance().GetSocket();

            socket.On(Socket.EVENT_CONNECT, () => { Debug.Log("Client connected to server"); })
                .On(Socket.EVENT_DISCONNECT, () => { Debug.Log("Client disconnected to server"); });

            socket.Connect();
        }

        // getter and setter
        public string InputEmail { get; set; }

        public string InputPassword { get; set; }

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

            return false;
        }

        public bool IsValidLogin()
        {
            if (!IsValidInput.isValidEmail(InputEmail))
                return false;
            if (!IsValidInput.IsValidPassword(InputPassword))
                return false;
            return true;
        }

        public void Authentication()
        {
            //Convert to JSONObject
            var userjson = new JObject();
            try
            {
                userjson.Add("email", InputEmail);
                userjson.Add("password", InputPassword);
            }
            catch (JsonException e)
            {
                Debug.Log(e.ToString());
            }

            Debug.Log("Email: " + InputEmail + ", Password: " + InputPassword);
            socket.Emit("authentication", userjson);

            // [SAMPLE] Method for receiving event from socket server
            HandleOnSocketEvent();
        }

        //[SAMPLE] Method handle "On" event from socket server
        public void HandleOnSocketEvent()
        {
            socket.On("authentication_result", args =>
            {
                var data = (JObject) args;
                if (data != null)
                {
                    Debug.Log("Authentication successed");
                    Debug.Log(data.ToString());
                }
                else
                {
                    Debug.Log("Authentication failed");
                }
            });

            socket.On("<event_name_1>", () =>
            {
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
        }
    }
}