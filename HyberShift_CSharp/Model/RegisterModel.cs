using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class RegisterModel
    {
        UserInfo userinfo;

        public UserInfo Info
        {
            get { return userinfo; }
            set { userinfo = value; }
        }

        public bool Register()
        {
            if (userinfo.isValid())
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
            //userinfo.Email = tfEmail.getText().toString();
            //userinfo.Password = tfPassword.getText().toString();
            //userinfo.Phone = tfPhoneNumber.getText().toString();
            //userinfo.FullName = tfName.getText().toString();
            //if (userinfo.AvatarRef != null)
            //    userinfo.AvatarRef = ImageUtils.encodeFileToBase64Binary(userinfo.AvatarRef);
            //else
            //    userinfo.AvatarRef = "null";

            ////Convert to JSONObject
            //JSONObject userjson = new JSONObject();
            //try
            //{
            //    //userjson.put("userid", userInfo.getUserid());
            //    userjson.put("email", userinfo.Email);
            //    userjson.put("fullname", userinfo.FullName);
            //    userjson.put("password", userinfo.Password);
            //    userjson.put("phone", userinfo.Phone);
            //    userjson.put("avatarstring", userinfo.AvatarRef);

            //    socket.emit("register", userjson);

            //}
            //catch (JSONException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}
        }

        public bool IsValidRegister()
        {
            //Console.WriteLine(userinfo.Email.Trim().Length);
            //if (userinfo.Email.Trim().Length == 0)
            //    return false;
            //if (userinfo.FullName.Trim().Length == 0)
            //    return false;
            //if (userinfo.Password.Trim().Length == 0)
            //    return false;
            //if (userinfo.ConfirmPassword.Trim().Length == 0)
            //    return false;
            //if (userinfo.Phone.Trim().Length == 0)
            //    return false;
            //if (userinfo.Password != userinfo.ConfirmPassword)
            //    return false;
            return true;
        }
    }
}
