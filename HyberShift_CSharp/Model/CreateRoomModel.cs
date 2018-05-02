using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class CreateRoomModel
    {
        private ListRoomModel listRoomModel;

        //Socket
        private readonly Socket socket;

        //User
        private readonly UserInfo userInfo;

        // constructor
        public CreateRoomModel()
        {
            listRoomModel = new ListRoomModel();
            userInfo = UserInfo.GetInstance();
            socket = SocketAPI.GetInstance().GetSocket();
        }

        // getter and setter
        public string InputRoomName { get; set; }

        public string[] InputEmailMember { get; set; }

        // method

        public void CreateRoom()
        {
            //Convert to JSONObject
            var roomjson = new JObject();

            JArray jarrayMember = new JArray();
            foreach (string member in InputEmailMember)
            {
                jarrayMember.Add(member);
            }

            try
            {
                roomjson.Add("room_name", InputRoomName);
                roomjson.Add("creator_name", userInfo.FullName);
                roomjson.Add("creator_email", userInfo.Email);
                roomjson.Add("members", jarrayMember);
            }
            catch (JsonException e)
            {
                Debug.Log(e.ToString());
            }

            Debug.Log("Creator_name: " + userInfo.FullName + ", Creator_email: " + userInfo.Email + ", Room Name: " + InputRoomName + ", Email members: " + jarrayMember);
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
                    invalid = (JArray) jsoninfo.GetValue("invalid");
                    jsarrMembers = (JArray) jsoninfo.GetValue("members");
                    Room room = new Room();
                    for (int i = 0; i < jsarrMembers.Count; i++)
                    {
                        room.AddMemebers(jsarrMembers.ElementAt(i).ToString());
                    }

                    //Add to listRoom
                    room.Name =  jsoninfo.GetValue("room_name").ToString();
                    room.ID = jsoninfo.GetValue("room_id").ToString();
                    listRoomModel.Add(room);

                    if (invalid.Count == 0)
                    {
                        MessageBox.Show("Create room successfully!", null, MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        CloseWindowManager.CloseCreateRoomWindow();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Create room successfully, emails " + invalid + " is not valid.", null, MessageBoxButton.OK,
                    //        MessageBoxImage.Warning);
                    //    return;
                    //}
                    Application.Current.Dispatcher.Invoke((Action)delegate {
                        // your code
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        CloseWindowManager.CloseCreateRoomWindow();
                        return;
                    });
                    //return;
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
                    string roomId = data.GetValue("room_id").ToString();
                    string roomName = data.GetValue("room_name").ToString();
                    Debug.Log("roomID: " + roomId + ", roomName: " + roomName);
                    listRoomModel.Add(new Room(roomId, roomName, null));
                    Debug.Log("Create room from: " + listRoomModel.NameList);
                }
                catch (JsonException e)
                {
                    Debug.Log(e.ToString());
                }
            });
        }

        public bool IsValidCreateRoom()
        {
            if (InputRoomName.Trim().Length == 0)
                return false;
            if (InputEmailMember.Length == 0)
                return false;

            return true;
        }
    }
}
