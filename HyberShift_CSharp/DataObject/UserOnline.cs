using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class UserOnline
    {
        private String name;
        private String email;

        public UserOnline()
        {

        }

        public UserOnline(String name, String email)
        {
            this.name = name;
            this.email = email;
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
