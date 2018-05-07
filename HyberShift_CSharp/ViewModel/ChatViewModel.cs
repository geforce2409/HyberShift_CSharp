using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    // View Model of ChatView
    public class ChatViewModel : BaseViewModel
    {
        private ListMessageModel listMessageModel;
        private RoomModel currentRoom;
        private Socket socket;
        private UserInfo userInfo;
        public ChatViewModel() : base()
        {
            listMessageModel = ListMessageModel.GetInstance();
            socket = SocketAPI.GetInstance().GetSocket();
            SendTextMessageCommand = new DelegateCommand(SendMessage);
            HandleSocket();
        }

        // getter and setter
        public DelegateCommand SendTextMessageCommand { get; set; }

        public ObservableCollection<MessageModel> ListMessage
        {
            get { return listMessageModel.List; }
            set
            {
                listMessageModel.List = value;
                NotifyChanged("ListMessage");
            }
        }

        public string Message { get; set; }
        public string RoomName //Display room's name
        {
            get { return currentRoom.Name; }

            // Implement this means user can change the room's name,
            // need to emit when setting and handle on server (not implement on server yet)
            // ...
        }

        public string Members // Display room's members
        {
            get
            {
                // convert from observablecollection to string
                string temp = "";
                foreach (string mem in currentRoom.Members)
                    temp += mem + " ";
                return temp;
            }
        }

        // method
        public void SendMessage()
        {
            if (Message.Trim().Length == 0)
                return;

            // Get name of the user from server
            var msgjson = new JObject();
            try
            {
                msgjson.Add("imgstring", userInfo.AvatarRef);
                msgjson.Add("sender", userInfo.FullName);
                msgjson.Add("message", Message);
                msgjson.Add("timestamp", DateTime.Now.Millisecond);

                if (currentRoom == null)
                    msgjson.Add("id", "public");
                else
                    msgjson.Add("id", currentRoom.ID);

                // Emit to server
                socket.Emit("new_message", msgjson);
            }
            catch (JsonException e)
            {	
                Debug.LogOutput(e.ToString());
            }

            Message = "";
        }

        private void HandleSocket()
        {
            socket.On("room_change", (args) =>
            {
                // handle data
                JObject data = (JObject) args;
                string idRoom = data.GetValue("id").ToString();
                JObject content = (JObject) data.GetValue("content");

                string id = content.GetValue("id").ToString();
                string sender = content.GetValue("sender").ToString();
                string message = content.GetValue("message").ToString();
                string imgstring = content.GetValue("imgstring").ToString();
                string filename = content.GetValue("filename").ToString();
                string filestring = content.GetValue("filestring").ToString();
                long timestamp = Convert.ToInt64(content.GetValue("timestamp"));

                Application.Current.Dispatcher.Invoke((Action) delegate
                {
                    // get current room from idRoom
                    currentRoom = ListRoomModel.GetInstance().GetRoomById(idRoom);

                    if (currentRoom.ID.Equals(id))
                    {
                        MessageModel msg = new MessageModel(id, message, sender, imgstring, filestring, filename,
                            timestamp);
                        listMessageModel.Add(msg);

                        Debug.LogOutput("Room: " + currentRoom.Name + " Message >> " + msg.Message);
                    }
                });
            });

            socket.On("new_message", (args) =>
            {
                var msgjson = (JObject)args;
                try
                {
                    // handle data
                    JObject data = (JObject)args;
                    string idRoom = data.GetValue("id").ToString();
                    JObject content = (JObject)data.GetValue("content");

                    string id = content.GetValue("id").ToString();
                    string sender = content.GetValue("sender").ToString();
                    string message = content.GetValue("message").ToString();
                    string imgstring = content.GetValue("imgstring").ToString();
                    string filename = content.GetValue("filename").ToString();
                    string filestring = content.GetValue("filestring").ToString();
                    long timestamp = Convert.ToInt64(content.GetValue("timestamp"));

                    MessageModel messageModel = new MessageModel(id, message, sender, imgstring, filename, filestring, timestamp);
                    if (id == "public")
                        Debug.LogOutput("public has new message");
                    //else
                    //{
                    //    // get current room from idRoom
                    //    currentRoom = ListRoomModel.GetInstance().GetRoomById(idRoom);

                    //    Debug.LogOutput(currentRoom.Name + " has new message");
                    //    // if user is in current room, then display
                    //    if (currentRoom.ID.Equals(id))
                    //    {
                    //        int indexToAdd = getMinIndexFrom(listTyping);
                    //        removeSenderFrom(listTyping, sender, lvMessage);
                    //        if (indexToAdd == 0)
                    //            lvMessage.getItems().add(message);
                    //        else
                    //            lvMessage.getItems().add(indexToAdd, message);

                    //        increaseIndexFrom(listTyping, indexToAdd);
                    //    }
                    //    else
                    //    {
                    //        Room updateRoom = listRoom.getRoomById(id);
                    //        int index = listRoom.getIndexOfRoom(id);
                    //        updateRoom.setNewMessage(true);

                    //        lvRoom.getItems().set(index, updateRoom);
                    //        System.out.println("Has new message!!!!!!!");
                    //    }
                    //}
                }
                catch (JsonException e)
                {
                    Debug.LogOutput(e.ToString());
                }
            });
        }
    }
}
