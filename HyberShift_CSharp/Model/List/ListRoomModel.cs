using System;
using System.Collections.ObjectModel;
using System.Linq;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model.List
{
    public class ListRoomModel : BaseList<RoomModel>
    {
        private static ListRoomModel instance;
        private ObservableCollection<RoomModel> list;

        //private ListRoomModel listRoomModel;

        // constructor
        public ListRoomModel()
        {
            list = new ObservableCollection<RoomModel>();
        }

        // getter and setter
        public ObservableCollection<object> IDList => GetCollectionOfField("ID");

        public ObservableCollection<object> NameList => GetCollectionOfField("Name");

        public ObservableCollection<object> MemberList => GetCollectionOfField("Members");

        public ObservableCollection<RoomModel> ListRoom
        {
            get { return list; }
            set { list = value; }
        }

        // singleton method
        public static ListRoomModel GetInstance()
        {
            if (instance == null)
                instance = new ListRoomModel();
            return instance;
        }

        //method
        public RoomModel GetRoomFromName(string roomName)
        {
            return GetFirstObjectByValue("Name", roomName);
        }

        public ObservableCollection<object> GetMembersFrom(string roomName)
        {
            return GetCollectionOfFieldByValue("Members", "Name", roomName);
        }

        public RoomModel GetRoomById(string id)
        {
            return GetFirstObjectByValue("ID", id);
        }

        public int GetIndexOfRoom(string id)
        {
            return GetIndexByValue("ID", id);
        }

        public ObservableCollection<string> GetMembersFrom(int indexRoom)
        {
            return GetFieldValueByIndex("Members", indexRoom);
        }

        public void Add(RoomModel element)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).Name.Equals(element.Name))
                    return;
            }
            list.Add(element);
        }
    }
}