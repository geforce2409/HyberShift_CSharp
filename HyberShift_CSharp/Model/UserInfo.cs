using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class UserInfo
    {
        private string userid;
        private string email;
        private string password;
        private string phone;
        private string fullName;
        private string avatarRef;

        // getter and setter
        private string UserId
        {
            get { return userid; }
            set { userid = value; }
        }

        private string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string AvatarRef
        {
            get { return avatarRef; }
            set { avatarRef = value; }
        }

        public bool isValid()
        {
            return true;
        }
    }
}
