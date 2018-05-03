using System.Collections.ObjectModel;
using System.Linq;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View.SignIn;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;


namespace HyberShift_CSharp.Model
{
    internal class MainModel
    {
        //list room
        ListRoomModel listRoomModel = ListRoomModel.GetInstance();
        RoomModel currRoom; // current Room

        private readonly Socket socket;

        // constructor
        public MainModel()
        {
            socket = SocketAPI.GetInstance().GetSocket();
        }

        // getter and setter

        //methods of model
        public void SignOut()
        {
            SignInPage signInPage = new SignInPage();
            signInPage.Show();
            CloseWindowManager.CloseMainWindow();
        }

        public void HandleOnSocketEvent()
        {
            socket.On("room_created", args =>
            {
                var data = (JObject) args;
                try
                {
                    string roomId = data.Values("room_id").ToString();
                    string roomName = data.Values("room_name").ToString();
                    JArray listjson = (JArray) data.GetValue("members");

                    ObservableCollection<string> members = new ObservableCollection<string>();
                    for (int i = 0; i < listjson.Count; i++)
                        members.Add(listjson.ElementAt(i).ToString());

                    listRoomModel.Add(new RoomModel(roomId, roomName, members));
                    Debug.Log("Register form: " + listRoomModel.NameList);
                    //TO-DO: Cập nhật list view
                }
                catch (JsonException e)
                {
                    Debug.Log(e.ToString());
                }
            });
        }
    }
}