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
using System.Windows.Controls;

namespace HyberShift_CSharp.ViewModel
{
    // View Model of ChatView
    public class ChatViewModel : BaseViewModel
    {
        private ListMessageModel listMessageModel;
        private ObservableCollection<string> sendersTyping;
        private RoomModel currentRoom;
        private Socket socket;
        private UserInfo userInfo;
        public ChatViewModel() : base()
        {
            currentRoom = new RoomModel();
            listMessageModel = ListMessageModel.GetInstance();
            sendersTyping = new ObservableCollection<string>();
            socket = SocketAPI.GetInstance().GetSocket();
            SendTextMessageCommand = new DelegateCommand(SendMessage);
            ItemSelectedCommand = new DelegateCommand<RoomModel>(HandleItemSelected);
            TypingCommand = new DelegateCommand<TextBox>(HandleTyping);
            DisplayTyping = "Hidden";
            userInfo = UserInfo.GetInstance();
            HandleSocket();
        }

        private void HandleTyping(TextBox textbox)
        {
            JObject data = new JObject();
            data.Add("sender", userInfo.FullName);
            data.Add("room_id", currentRoom.ID);

            if (textbox.Text.Length > 0)
            {
                
                socket.Emit("is_typing", data);
            }
            else
            {
                socket.Emit("done_typing", data);
            }
        }

        private void HandleItemSelected(RoomModel obj)
        {
            currentRoom = obj;
            RoomName = currentRoom.Name;
        }

        // getter and setter
        public DelegateCommand SendTextMessageCommand { get; set; }
        public DelegateCommand<RoomModel> ItemSelectedCommand { get; set; }
        public DelegateCommand<TextBox> TypingCommand { get; set; }

        public ObservableCollection<MessageModel> ListMessage
        {
            get { return listMessageModel.List; }
            set
            {
                listMessageModel.List = value;
                NotifyChanged("ListMessage");
            }
        }

        public string Message
        {
            get;
            set;
        }
        public string RoomName //Display room's name
        {
            get { return currentRoom.Name; }
            set
            {
                currentRoom.Name = value;
                NotifyChanged("RoomName");
            }
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

        public string DisplayTyping { get; set; }

        public string TypingMessage
        {
            get
            {
                //convert list senderstyping to string
                if (sendersTyping.Count == 0)
                {
                    DisplayTyping = "Hidden";
                    NotifyChanged("DisplayTyping");
                    return string.Empty;
                }

                DisplayTyping = "Visible";
                NotifyChanged("DisplayTyping");
                if (sendersTyping.Count == 1)
                    return sendersTyping[0] + " is typing . . .";

                string rs = string.Empty;
                for (int i = 0; i < sendersTyping.Count - 1; i++)
                    rs += sendersTyping[i] + ", ";
                rs += sendersTyping[sendersTyping.Count - 1] + " are typing . . .";
                return rs;
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
                //msgjson.Add("imgstring", userInfo.AvatarRef);
                msgjson.Add("imgstring", userInfo.AvatarRef);
                msgjson.Add("sender", userInfo.FullName);
                msgjson.Add("message", Message);
                msgjson.Add("timestamp", DateTime.Now.Ticks);
                msgjson.Add("filename", "null");
                msgjson.Add("filestring", "null");

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
            NotifyChanged("Message");
        }

        private void HandleSocket()
        {
            //socket.On("room_change", (roomId) =>
            //{
            //    currentRoom = ListRoomModel.GetInstance().GetRoomById(roomId.ToString());
            //    RoomName = currentRoom.Name;
            //    Debug.LogOutput("Selected room: " + "room id: " + currentRoom.ID + " room name: " + RoomName);
            //});

            socket.On("init_message", (args) =>
            {
                // handle data            
                JObject content = (JObject)args;

                string id = content.GetValue("id").ToString();
                string messageid = content.GetValue("message_id").ToString();
                string sender = content.GetValue("sender").ToString();
                string message = content.GetValue("message").ToString();
                string imgstring = content.GetValue("imgstring").ToString();
                string filename = content.GetValue("filename").ToString();
                string filestring = content.GetValue("filestring").ToString();
                long timestamp = Convert.ToInt64(content.GetValue("timestamp"));

                Application.Current.Dispatcher.Invoke((Action) delegate
                {
                    //if user is in the room that occur event, display message
                    if (currentRoom.ID.Equals(id))
                    {
                        MessageModel msg = new MessageModel(id, messageid, message, sender, imgstring, filestring, filename,
                            timestamp);
                        listMessageModel.AddWithCheck(msg, "MessageID");

                        Debug.LogOutput("Room: " + currentRoom.Name + " Message >> " + msg.Message);
                    }
                });
            });

            socket.On("is_typing", (arg) =>
            {
                JObject data = (JObject)arg;
                string sender = data.GetValue("sender").ToString();
                string roomId = data.GetValue("room_id").ToString();

                //if user is not in the room that occur event, then return
                if (!currentRoom.ID.Equals(roomId))
                    return;

                //check and add to senderstyping
                foreach (string sd in sendersTyping)
                    if (sd.Equals(sender))
                        return;
                sendersTyping.Add(sender);
                NotifyChanged("TypingMessage");
            });

            socket.On("done_typing", (arg) =>
            {
                JObject data = (JObject)arg;
                string sender = data.GetValue("sender").ToString();
                string roomId = data.GetValue("room_id").ToString();

                //if user is not in the room that occur event, then return
                if (!currentRoom.ID.Equals(roomId))
                    return;

                sendersTyping.Remove(sender);
                NotifyChanged("TypingMessage");
            });

            //socket.On("new_message", (args) =>
            //{
            //    var msgjson = (JObject)args;
            //    try
            //    {
            //        // handle data
            //        JObject data = (JObject)args;
            //        string idRoom = data.GetValue("id").ToString();
            //        JObject content = (JObject)data.GetValue("content");

            //        string id = content.GetValue("id").ToString();
            //        string sender = content.GetValue("sender").ToString();
            //        string message = content.GetValue("message").ToString();
            //        string imgstring = content.GetValue("imgstring").ToString();
            //        string filename = content.GetValue("filename").ToString();
            //        string filestring = content.GetValue("filestring").ToString();
            //        long timestamp = Convert.ToInt64(content.GetValue("timestamp"));

            //        MessageModel messageModel = new MessageModel(id, message, sender, imgstring, filename, filestring, timestamp);
            //        if (id == "public")
            //            Debug.LogOutput("public has new message");
            //        //else
            //        //{
            //        //    // get current room from idRoom
            //        //    currentRoom = ListRoomModel.GetInstance().GetRoomById(idRoom);

            //        //    Debug.LogOutput(currentRoom.Name + " has new message");
            //        //    // if user is in current room, then display
            //        //    if (currentRoom.ID.Equals(id))
            //        //    {
            //        //        int indexToAdd = getMinIndexFrom(listTyping);
            //        //        removeSenderFrom(listTyping, sender, lvMessage);
            //        //        if (indexToAdd == 0)
            //        //            lvMessage.getItems().add(message);
            //        //        else
            //        //            lvMessage.getItems().add(indexToAdd, message);

            //        //        increaseIndexFrom(listTyping, indexToAdd);
            //        //    }
            //        //    else
            //        //    {
            //        //        Room updateRoom = listRoom.getRoomById(id);
            //        //        int index = listRoom.getIndexOfRoom(id);
            //        //        updateRoom.setNewMessage(true);

            //        //        lvRoom.getItems().set(index, updateRoom);
            //        //        System.out.println("Has new message!!!!!!!");
            //        //    }
            //        //}
            //    }
            //    catch (JsonException e)
            //    {
            //        Debug.LogOutput(e.ToString());
            //    }
            //});
        }
    }
}
