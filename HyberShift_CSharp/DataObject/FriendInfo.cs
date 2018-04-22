using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class FriendInfo
    {
        private String id;
        private String name;
        private String email;
        private String imgstring;

        public FriendInfo()
        {

        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string ImgString
        {
            get { return imgstring; }
            set { imgstring = value; }
        }
    }
}
