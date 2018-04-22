using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class RegisterModel
    {
        UserInfo userinfo;
        private string confirmPassword;
        Socket socket = SocketAPI.GetInstance().GetSocket();

        // constructor
        public RegisterModel()
        {
            userinfo = new UserInfo();
            confirmPassword = "";
        }

        // getter and setter
        public UserInfo Info
        {
            get { return userinfo; }
            set { userinfo = value; }
        }

        public String ConfirmPassword
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
            JObject userjson = new JObject();
            try
            {
                //userjson.put("userid", userInfo.getUserid());
                userjson.Add("email", userinfo.Email);
                userjson.Add("fullname", userinfo.FullName);
                userjson.Add("password", userinfo.Password);
                userjson.Add("phone", userinfo.Phone);
                userjson.Add("avatarstring", userinfo.AvatarRef);

                socket.Emit("register", userjson);

            }
            catch (JsonException e)
            {
                // TODO Auto-generated catch block
                Debug.Log(e.ToString());
            }
        }

        public bool IsValidRegister()
        {
            Console.WriteLine(userinfo.Email.Trim().Length);
            if (userinfo.Email.Trim().Length == 0)
                return false;
            if (userinfo.FullName.Trim().Length == 0)
                return false;
            if (userinfo.Password.Trim().Length < 6)
                return false;
            if (ConfirmPassword.Trim().Length < 6)
                return false;
            if (userinfo.Phone.Trim().Length == 0)
                return false;
            if (userinfo.Password != ConfirmPassword)
                return false;
            return true;
        }
    }
}
