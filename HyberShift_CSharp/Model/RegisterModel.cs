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

                PushData();

                return true;
            }

            return false;
        }

        public void PushData()
        {
            //Info.Email = tfEmail.getText().toString();
            //Info.Password = tfPassword.getText().toString();
            //Info.Phone = tfPhoneNumber.getText().toString();
            //Info.FullName = tfName.getText().toString();
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
                // TODO Auto-generated catch block
                Debug.Log(e.ToString());
            }

            Debug.Log("Email: " + Info.Email + ", Password: " + Info.Password + ", ConfirmPassword: " +
                      ConfirmPassword + ", Name: " + Info.FullName + ", Phone: " + Info.Phone);
        }

        public bool IsValidRegister()
        {
            Console.WriteLine(Info.Email.Trim().Length);
            if (Info.Email.Trim().Length == 0)
                return false;
            if (Info.FullName.Trim().Length == 0)
                return false;
            if (Info.Password.Trim().Length < 6)
                return false;
            if (ConfirmPassword.Trim().Length < 6)
                return false;
            if (Info.Phone.Trim().Length == 0)
                return false;
            if (Info.Password != ConfirmPassword)
                return false;
            return true;
        }

        public void HandleOnSocketEvent()
        {
            socket.On("register_result", () =>
            {
                if (Register())
                    //userinfo.UserId = ...;
                    Debug.Log("Register successed");
                else
                    Debug.Log("Register failed");
            });
        }
    }
}