using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class Message
    {
        private String id;
        private String message;
        private String sender;  // name of sender
        private String imgstring;   //image string of sender (base64)
        private int timestamp;

        public Message()
        {

        }

        public Message(String id, String message, String sender, String imgstring, int timestamp)
        {
            this.id = id;
            this.message = message;
            this.sender = sender;
            this.timestamp = timestamp;
            this.imgstring = imgstring;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string MessageContent
        {
            get { return message; }
            set { message = value; }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public int TimeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string ImgString
        {
            get { return imgstring; }
            set { imgstring = value; }
        }
    }
}