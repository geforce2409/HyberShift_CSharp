using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class ListNotification
    {
        private static ListNotification instance = null;
        private List<Notification> list;

        public ListNotification()
        {
            list = new List<Notification>();
        }

        public static ListNotification getInstance()
        {
            if (instance == null)
                instance = new ListNotification();
            return instance;
        }

        public List<Notification> getNotificationList()
        {
            return this.list;
        }

        public void clear()
        {
            list.Clear();
        }

        public void addNotification(Notification notification)
        {
            //check
            for (int i = 0; i < list.Count(); i++)
            {
                if (notification.Equals(list.ElementAt(i)))
                    return;
            }

            list.Add(notification);
        }

        //public ObservableList<Notification> getOList()
        //{
        //    return FXCollections.observableArrayList(this.list);
        //}
    }

}
