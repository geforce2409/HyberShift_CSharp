using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model.List
{
    public class ListRoomModel : BaseList<RoomModel>, INotifyPropertyChanged
    {
        private static ListRoomModel instance;
        private Socket socket;

        public event PropertyChangedEventHandler PropertyChanged;

        //private ListRoomModel listRoomModel;

        // constructor
        public ListRoomModel()
        {
            list = new ObservableCollection<RoomModel>();
            socket = SocketAPI.GetInstance().GetSocket();
            // received list at the begining
            HandleSocket();
        }

        // getter and setter
        public ObservableCollection<object> IDList => GetCollectionOfField("ID");

        public ObservableCollection<object> NameList => GetCollectionOfField("Name");

        public ObservableCollection<object> MemberList => GetCollectionOfField("Members");

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

        private void HandleSocket()
        {  
            socket.On("room_created", (args) =>
            {  
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    JObject obj = (JObject)args;
                    try
                    {
                        string roomId = obj.GetValue("room_id").ToString();
                        string roomName = obj.GetValue("room_name").ToString();
                        JArray listjson = (JArray)obj.GetValue("members");
                        ObservableCollection<string> members = new ObservableCollection<string>();
                        for (int i = 0; i < listjson.Count; i++)
                        {
                            members.Add(listjson.ElementAt(i).ToString());
                        }
                        list.Add(new RoomModel(roomId, roomName, members));
                   
                    //Debug.Log("ListRoomModel >> " + this.ToString());
                    //Debug.LogOutput("ListRoomModel >> " + this.ToString());

                    //test
                    Debug.LogOutput("======================");
                    Debug.LogOutput("Room id: " + roomId);
                    Debug.LogOutput("Room name: " + roomName);

                    }
                    catch (JsonException e)
                    {
                        // TODO Auto-generated catch block
                        Debug.Log("ListRoomModel exception >> " + e);
                        Debug.LogOutput("ListRoomModel exception >> " + e);
                    }
                });
            });
        }

        public override string ToString()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            foreach(RoomModel room in List)
            {
                result.Add("Room ID: " + room.ID + " | Room Name: " + room.Name + " | Members: " + room.Members);
            }
            return " ";
        }
        
    }
}