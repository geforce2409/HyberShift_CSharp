using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class Notification
    {
        private String sender;  //name of sender
        private String imgstring;
        private String content;
        private String type;    // type of notification
        private int timestamp;

        public Notification()
        {

        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public string ImgString
        {
            get { return imgstring; }
            set { imgstring = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public int TimeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
    }
}
