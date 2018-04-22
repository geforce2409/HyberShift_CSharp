using System.Collections.Generic;
using System.Linq;

namespace HyberShift_CSharp.Model
{
    public class ListFriendInfo
    {
        private static ListFriendInfo instance;
        private readonly List<FriendInfo> list;

        public ListFriendInfo()
        {
            list = new List<FriendInfo>();
        }

        public static ListFriendInfo getInstance()
        {
            if (instance == null)
                instance = new ListFriendInfo();
            return instance;
        }

        public void addFriendInfo(FriendInfo info)
        {
            for (var i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).Equals(info))
                    return;

            list.Add(info);
        }

        //public ObservableCollection<FriendInfo> getOList()
        //{
        //    return FXCollections.observableArrayList(this.list);
        //}
    }
}