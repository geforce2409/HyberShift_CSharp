using System;
using System.Collections.ObjectModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View.Dialog;
using HyberShift_CSharp.View.SignIn;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.ViewModel
{
    // View Model of ChatView
    public class ChatViewModel : BaseViewModel
    {
        private RoomModel currentRoom;
        private readonly DialogService dialogService;
        private readonly ListMessageModel listMessageModel;
        private readonly ObservableCollection<string> sendersTyping;
        private readonly Socket socket;
        private readonly UserInfo userInfo;

        public ChatViewModel()
        {
            dialogService = new DialogService();
            currentRoom = new RoomModel();
            listMessageModel = ListMessageModel.GetInstance();
            sendersTyping = new ObservableCollection<string>();
            socket = SocketAPI.GetInstance().GetSocket();
            SendTextMessageCommand = new DelegateCommand(SendMessage);
            ItemSelectedCommand = new DelegateCommand<RoomModel>(HandleItemSelected);
            TypingCommand = new DelegateCommand<TextBox>(HandleTyping);
            ChangeImageCommand = new DelegateCommand(ChangeImage);
            SignOutCommand = new DelegateCommand(SignOut);
            DisplayTyping = "Hidden";
            userInfo = UserInfo.GetInstance();
            HandleSocket();
        }

        // getter and setter
        public DelegateCommand SendTextMessageCommand { get; set; }
        public DelegateCommand<RoomModel> ItemSelectedCommand { get; set; }
        public DelegateCommand<TextBox> TypingCommand { get; set; }
        public DelegateCommand ChangeImageCommand { get; set; }
        public DelegateCommand SignOutCommand { get; set; }

        public ObservableCollection<MessageModel> ListMessage
        {
            get => listMessageModel.List;
            set
            {
                listMessageModel.List = value;
                NotifyChanged("ListMessage");
            }
        }

        public string Message { get; set; }

        public string UserName => UserInfo.GetInstance().FullName;

        public string RoomName //Display room's name
        {
            get => currentRoom.Name;
            set
            {
                currentRoom.Name = value;
                NotifyChanged("RoomName");
            }
            // Implement this means user can change the room's name,
            // need to emit when setting and handle on server (not implement on server yet)
            // ...
        }

        public BitmapImage Photo
        {
            get
            {
                if (userInfo.AvatarRef == "null")
                    return null;
                return ImageUtils.Base64StringToBitmapSource(userInfo.AvatarRef);
            }
            set
            {
                Photo = ImageUtils.Base64StringToBitmapSource(userInfo.AvatarRef);
                NotifyChanged("Photo");
            }
        }

        public string Members // Display room's members
        {
            get
            {
                // convert from observablecollection to string
                var temp = "";
                foreach (var mem in currentRoom.Members)
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

                var rs = string.Empty;
                for (var i = 0; i < sendersTyping.Count - 1; i++)
                    rs += sendersTyping[i] + ", ";
                rs += sendersTyping[sendersTyping.Count - 1] + " are typing . . .";
                return rs;
            }
        }

        private void HandleTyping(TextBox textbox)
        {
            var data = new JObject();
            data.Add("sender", userInfo.FullName);
            data.Add("room_id", currentRoom.ID);

            if (textbox.Text.Length > 0)
                socket.Emit("is_typing", data);
            else
                socket.Emit("done_typing", data);
        }

        private void HandleItemSelected(RoomModel obj)
        {
            currentRoom = obj;
            RoomName = currentRoom.Name;
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

        public void ChangeImage()
        {
            SystemSounds.Exclamation.Play();

            var path = dialogService.OpenFile("Choose image file", "Image (.png ,.jpg)|*.png;*.jpg");

            if (path == "")
                return;

            //string encodstring = ImageUtils.CopyImageToBase64String(Image.FromFile(path));

            ////TO-DO đưa hình ảnh dc chọn lên server và 
            ////userInfo.AvatarRef = encodstring;

            //Photo = ImageUtils.Base64StringToBitmapSource(encodstring);
            //NotifyChanged("Photo");
        }

        public void SignOut()
        {
            // Chưa chạy chuẩn
            var confirmDialog = new ConfirmDialog("SIGN OUT", "Are you really want to sign out?", () =>
            {
                var signInPage = new SignInPage();
                signInPage.Show();
                CloseWindowManager.CloseMainWindow();
            });
            confirmDialog.Show();
        }

        private void HandleSocket()
        {
            //socket.On("room_change", (roomId) =>
            //{
            //    currentRoom = ListRoomModel.GetInstance().GetRoomById(roomId.ToString());
            //    RoomName = currentRoom.Name;
            //    Debug.LogOutput("Selected room: " + "room id: " + currentRoom.ID + " room name: " + RoomName);
            //});

            socket.On("init_message", args =>
            {
                // handle data            
                var content = (JObject) args;

                var id = content.GetValue("id").ToString();
                var messageid = content.GetValue("message_id").ToString();
                var sender = content.GetValue("sender").ToString();
                var message = content.GetValue("message").ToString();
                var imgstring = content.GetValue("imgstring").ToString();
                var filename = content.GetValue("filename").ToString();
                var filestring = content.GetValue("filestring").ToString();
                var timestamp = Convert.ToInt64(content.GetValue("timestamp"));

                Application.Current.Dispatcher.Invoke(delegate
                {
                    //if user is in the room that occur event, display message
                    if (currentRoom.ID.Equals(id))
                    {
                        var msg = new MessageModel(id, messageid, message, sender, imgstring, filestring, filename,
                            timestamp);
                        listMessageModel.AddWithCheck(msg, "MessageID");

                        Debug.LogOutput("Room: " + currentRoom.Name + " Message >> " + msg.Message);
                    }
                });
            });

            socket.On("new_message", arg =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    // handle data            
                    var content = (JObject) arg;

                    var id = content.GetValue("id").ToString();

                    if (currentRoom.ID.Equals(id))
                        return;

                    // find room has the id
                    var index = ListRoomModel.GetInstance().GetIndexByValue("ID", id);
                    ListRoomModel.GetInstance().List[index].DisplayNewMessage = "Visible";
                    ListRoomModel.GetInstance().List[index].NotifyChanged("DisplayNewMessage");
                    ListRoomModel.GetInstance().NotifyChanged("List");

                    SystemSounds.Beep.Play();
                });
            });

            socket.On("is_typing", arg =>
            {
                var data = (JObject) arg;
                var sender = data.GetValue("sender").ToString();
                var roomId = data.GetValue("room_id").ToString();

                //if user is not in the room that occur event, then return
                if (!currentRoom.ID.Equals(roomId))
                    return;

                //check and add to senderstyping
                foreach (var sd in sendersTyping)
                    if (sd.Equals(sender))
                        return;
                sendersTyping.Add(sender);
                NotifyChanged("TypingMessage");
            });

            socket.On("done_typing", arg =>
            {
                var data = (JObject) arg;
                var sender = data.GetValue("sender").ToString();
                var roomId = data.GetValue("room_id").ToString();

                //if user is not in the room that occur event, then return
                if (!currentRoom.ID.Equals(roomId))
                    return;

                sendersTyping.Remove(sender);
                NotifyChanged("TypingMessage");
            });
        }
    }
}