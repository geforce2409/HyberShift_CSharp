using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class LoginModel
    {
        private readonly Socket socket;
        ListRoomModel listRoomModel = ListRoomModel.GetInstance();
        UserInfo userInfo = UserInfo.GetInstance();
        // constructor
        public LoginModel()
        {
            InputEmail = "";
            InputPassword = "";
            socket = SocketAPI.GetInstance().GetSocket();

            socket.On(Socket.EVENT_CONNECT, () => { Debug.Log("Client connected to server"); })
                .On(Socket.EVENT_DISCONNECT, () => { Debug.Log("Client disconnected to server"); });

            socket.Connect();
        }

        // getter and setter
        public string InputEmail { get; set; }

        public string InputPassword { get; set; }

        public bool LogIn()
        {
            // TO-DO: use socket to login user

            // if success then return true
            if (IsValidLogin())
            {
                Authentication();
                return true;
            }

            // else return false

            return false;
        }

        public bool IsValidLogin()
        {
            if (!IsValidInput.isValidEmail(InputEmail))
                return false;
            if (!IsValidInput.IsValidPassword(InputPassword))
                return false;
            return true;
        }

        public void Authentication()
        {
            //Convert to JSONObject
            var userjson = new JObject();
            try
            {
                userjson.Add("email", InputEmail);
                userjson.Add("password", InputPassword);
            }
            catch (JsonException e)
            {
                Debug.Log(e.ToString());
            }

            Debug.Log("Email: " + InputEmail + ", Password: " + InputPassword);
            socket.Emit("authentication", userjson);

            // [SAMPLE] Method for receiving event from socket server
            HandleOnSocketEvent();
        }

        //[SAMPLE] Method handle "On" event from socket server
        public void HandleOnSocketEvent()
        {
            socket.On("authentication_result", args =>
            {
                var data = (JObject) args;
                if (data != null)
                {
                    Debug.Log("Authentication successed");
                    Debug.Log(data.ToString());

                    // [IMPORTANT] set info for userinfo
                    userInfo.AvatarRef = data.GetValue("avatarstring").ToString();
                    userInfo.FullName = data.GetValue("fullname").ToString();
                    userInfo.Email = data.GetValue("email").ToString();
                    userInfo.Phone = data.GetValue("phone").ToString();

                    Application.Current.Dispatcher.Invoke((Action) delegate
                    {
                        // your code
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();

                        CreateRoom a = new CreateRoom();
                        a.Show();
                        CloseWindowManager.CloseLoginWindow();
                    });
                    return;
                }
                else
                {
                    Debug.Log("Authentication failed");
                }
            }).On("room_created", args =>
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
                }
                catch (JsonException e)
                {
                    Debug.Log(e.ToString());
                }
            });
        }
    }
}