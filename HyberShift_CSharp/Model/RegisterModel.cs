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
                return true;
            }

            return false;
        }
    }
}
