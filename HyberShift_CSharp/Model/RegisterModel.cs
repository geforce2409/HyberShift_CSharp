using System;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class RegisterModel
    {
        private readonly Socket socket = SocketAPI.GetInstance().GetSocket();
        private string confirmPassword;

        // constructor
        public RegisterModel()
        {
            Info = new UserInfo();
            Info.Email = "";
            Info.Password = "";
            Info.Phone = "";
            Info.FullName = "";
            //if (Info.AvatarRef != null)
            //    Info.AvatarRef = ImageUtils.encodeFileToBase64Binary(Info.AvatarRef);
            //else
            Info.AvatarRef = "null";
            confirmPassword = "";
        }

        // getter and setter
        public UserInfo Info { get; set; }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; }
        }

        public bool Register()
        {
            if (IsValidRegister())
            {
                // TO-DO: using socket to register with server here

                // if success then return true

                //PushData();

                return true;
            }

            return false;
        }

        public void PushData()
        {
            //if (Info.AvatarRef != null)
            //    Info.AvatarRef = ImageUtils.encodeFileToBase64Binary(userinfo.AvatarRef);
            //else
            //    Info.AvatarRef = "null";

            //Convert to JSONObject
            var userjson = new JObject();
            try
            {
                //userjson.put("userid", userInfo.getUserid());
                userjson.Add("email", Info.Email);
                userjson.Add("fullname", Info.FullName);
                userjson.Add("password", Info.Password);
                userjson.Add("phone", Info.Phone);
                userjson.Add("avatarstring", Info.AvatarRef);

                socket.Emit("register", userjson);
            }
            catch (JsonException e)
            {
                Debug.Log(e.ToString());
            }

            Debug.Log("Email: " + Info.Email + ", Password: " + Info.Password + ", ConfirmPassword: " +
                      ConfirmPassword + ", Name: " + Info.FullName + ", Phone: " + Info.Phone + 
                      ", AvatarString: " + Info.AvatarRef);

            HandleOnSocketEvent();
        }

        public bool IsValidRegister()
        {
            if (!IsValidInput.isValidEmail(Info.Email))
                return false;
            if (!IsValidInput.IsValidPassword(Info.Password))
                return false;
            if (!IsValidInput.IsValidPassword(ConfirmPassword))
                return false;
            if (!IsValidInput.IsValidFullName(Info.FullName))
                return false;
            if (!IsValidInput.IsValidPhone(Info.Phone)) 
                return false;
            if (Info.Password != ConfirmPassword)
                return false;
            return true;
        }

        public void HandleOnSocketEvent()
        {
            socket.On("register_result", new Action<object>(args =>
            {
                string data = (string) args;
                if (data != null)
                {
                    Info.UserId = data;
                    Debug.Log(data.ToString());
                    Debug.Log("Register successed");
                }
                else
                    Debug.Log("Register failed");
            }));
        }
    }
}