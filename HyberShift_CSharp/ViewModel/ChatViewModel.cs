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

namespace HyberShift_CSharp.ViewModel
{
    // View Model of ChatView
    public class ChatViewModel: BaseViewModel
    {
        private ListMessageModel listMessageModel;
        private RoomModel currentRoom;
        private Socket socket;

        public ChatViewModel() : base()
        {
            listMessageModel = ListMessageModel.GetInstance();
            socket = SocketAPI.GetInstance().GetSocket();

            HandleSocket();
        }

        // getter and setter
        public ObservableCollection<MessageModel> ListMessage
        {
            get { return listMessageModel.List; }
            set { listMessageModel.List = value; NotifyChanged("ListMessage"); }
        }

        public string RoomName //Display room's name
        {
            get { return currentRoom.Name; }
            
            // Implement this means user can change the room's name,
            // need to emit when setting and handle on server (not implement on server yet)
            // ...
        }

        public string Members   // Display room's members
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
        private void HandleSocket()
        {
            socket.On("room_change", (args) =>
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

                Application.Current.Dispatcher.Invoke((Action)delegate
               {
                   // get current room from idRoom
                   currentRoom = ListRoomModel.GetInstance().GetRoomById(idRoom);

                   if (currentRoom.ID.Equals(id))
                   {
                       MessageModel msg = new MessageModel(id, message, sender, imgstring, filestring, filename, timestamp);
                       listMessageModel.Add(msg);

                       Debug.LogOutput("Room: " + currentRoom.Name + " Message >> " + msg.Message);
                   }
               });
            });
        }
    }
}
