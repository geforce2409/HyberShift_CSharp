using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class CreateRoomModel
    {
        //Socket
        private readonly Socket socket;

        //User
        private readonly UserInfo userInfo;

        // constructor
        public CreateRoomModel()
        {
            InputRoomName = "";
            InputEmailMember = "";

            userInfo = UserInfo.getInstance();
            socket = SocketAPI.GetInstance().GetSocket();
        }

        // getter and setter
        public string InputRoomName { get; set; }

        public string InputEmailMember { get; set; }

        // method

        public void CreateRoom()
        {
            //Convert to JSONObject
            var roomjson = new JObject();

            try
            {
                roomjson.Add("room_name", InputRoomName);
                roomjson.Add("creator_name", userInfo.FullName);
                roomjson.Add("creator_email", userInfo.Email);

                for (int i = 0; i < InputEmailMember.Length; i++)
                    roomjson.Add("members", InputEmailMember[i]);
            }
            catch (JsonException e)
            {
                Debug.Log(e.ToString());
            }

            Debug.Log("Room Name: " + InputRoomName + ", Email members: " + InputEmailMember);
            socket.Emit("create_room", roomjson);

            // [SAMPLE] Method for receiving event from socket server
            HandleOnSocketEvent();
        }

        //[SAMPLE] Method handle "On" event from socket server
        public void HandleOnSocketEvent()
        {
            socket.On("create_room_result", args =>
            {
                var jsoninfo = (JObject)args;
                JArray invalid;
                JArray jsarrMembers;

                try
                {
                    //invalid = jsoninfo.getJSONArray("invalid");
                    //jsarrMembers = jsoninfo.getJSONArray("members");
                    //Room room = new Room();
                    //for (int i = 0; i < jsarrMembers.Count; i++)
                    //{
                    //    room.AddMemebers(jsarrMembers.ElementAt(i).ToString());
                    //}

                    //if (invalid.Count == 0)
                    //{
                    //    CloseWindowManager.CloseCreateRoomWindow();
                    //}
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    CloseWindowManager.CloseCreateRoomWindow();

                }
                catch (JsonException e)
                {
                    Debug.Log(e.ToString());
                }
            }).On("room_created", args =>
            {
                var data = (JObject)args;
                try
                {
                    //string roomId = data.getString("room_id");
                    //string roomName = data.getString("room_name");
                    //Debug.Log("roomName: " + roomName);
                    //listRoomModel.AddRoom(new Room(roomId, roomName, null));
                    //Debug.Log("Create room from: " + listRoomModel.NameList);
                }
                catch (JsonException e)
                {
                    Debug.Log(e.ToString());
                }
            });
        }

        private bool isValidCreateRoom()
        {
            if (InputRoomName.Trim().Length == 0)
                return false;
            if (InputEmailMember.Length == 0)
                return false;

            return true;
        }
    }
}
