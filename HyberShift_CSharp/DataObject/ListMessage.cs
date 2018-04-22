using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HyberShift_CSharp.Model
{
    public class ListMessage
    {
        private static ListMessage instance;
        private List<Message> list;

        public ListMessage()
        {
            list = new List<Message>();
        }

        static public ListMessage getInstance()
        {
            if (instance == null)
                instance = new ListMessage();
            return instance;
        }

        public void addMessage(Message msg)
        {
            //check exist
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).TimeStamp == msg.TimeStamp)
                    return;
            }
            list.Add(msg);
        }

        public void setListMessage(List<Message> list) { this.list = list; }

        public List<Message> getList() { return list; }

        public ArrayList getListMessage()
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                result.Add(list.ElementAt(i).MessageContent);
            }

            return result;
        }

        public ArrayList getListSender()
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                result.Add(list.ElementAt(i).Sender);
            }

            return result;
        }


        public Message getMessageFromId(String id)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                Message temp = list.ElementAt(i);
                if (temp.ID.Equals(id))
                    return temp;
            }

            return null;
        }

        public Message getMessageFromSender(String sender)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                Message temp = list.ElementAt(i);
                if (temp.Sender.Equals(sender))
                    return temp;
            }

            return null;
        }

        //public ObservableList<Message> getOList()
        //{
        //    return FXCollections.observableArrayList(this.list);
        //}

        //public ObservableList<String> getOListSender()
        //{
        //    ObservableList<String> olist = FXCollections.observableArrayList(this.getListSender());
        //    return olist;
        //}

        //public ObservableList<String> getOListMessage()
        //{
        //    ArrayList msgList = new ArrayList();
        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        String msg = list.ElementAt(i).Sender + ": " + list.ElementAt(i).MessageContent;
        //        msgList.Add(msg);
        //    }
        //    ObservableList<String> olist = FXCollections.observableArrayList(msgList);
        //    return olist;
        //}

    }
}
