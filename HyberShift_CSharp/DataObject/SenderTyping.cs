using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class SenderTyping
    {
        private String senderName;  //name of sender
        private int index;          //index of "typing" message in listview

        public SenderTyping()
        {

        }

        public SenderTyping(String senderName, int index)
        {
            this.senderName = senderName;
            this.index = index;
        }

        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    }
}
