using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;

namespace HyberShift_CSharp.Model
{
    public class ListFriendInfo
    {

        private static ListFriendInfo instance = null;
        private List<FriendInfo> list;

        public static ListFriendInfo getInstance()
        {
            if (instance == null)
                instance = new ListFriendInfo();
            return instance;
        }

        public ListFriendInfo()
        {
            list = new List<FriendInfo>();
        }

        public void addFriendInfo(FriendInfo info)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).Equals(info))
                    return;
            }

            list.Add(info);
        }

        //public ObservableCollection<FriendInfo> getOList()
        //{
        //    return FXCollections.observableArrayList(this.list);
        //}
    }
}
