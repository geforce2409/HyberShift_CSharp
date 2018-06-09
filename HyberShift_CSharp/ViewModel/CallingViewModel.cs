using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View.Calling;
using HyberShift_CSharp.View.Dialog;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.ViewModel
{
    public class CallingViewModel : BaseViewModel
    {
        private readonly CallingModel callingModel;
        private RoomModel currentRoom;
        private int flagShowVoiceCall;
        private object selectedViewModel;
        private readonly Socket socket;

        public CallingViewModel()
        {
            socket = SocketAPI.GetInstance().GetSocket();
            currentRoom = new RoomModel();
            callingModel = CallingModel.GetInstace(currentRoom);
            RoomChangeCommand = new DelegateCommand<RoomModel>(OnRoomChange);
            LoadCommand = new DelegateCommand(OnLoad);

            ShowVoiceCallCommand = new DelegateCommand(ShowVoiceCall);

            HandleSocket();
        }

        public CallingViewModel(object viewmodel) : this()
        {
            SelectedViewModel = viewmodel;
        }

        public DelegateCommand ShowVoiceCallCommand { get; set; }
        public DelegateCommand<RoomModel> RoomChangeCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }

        public RoomModel CurrentRoom
        {
            get => currentRoom;
            set
            {
                currentRoom = value;
                NotifyChanged("CurrentRoom");
                NotifyChanged("RoomName");
                SelectedViewModel = new WaitingCallViewModel(ViewModelNavigator, currentRoom);
            }
        }

        public string RoomName => currentRoom.Name;

        public object SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                NotifyChanged("SelectedViewModel");
            }
        }

        public void ViewModelNavigator(object obj, params object[] parameters)
        {
            if (obj.ToString() == "WaitingCallViewModel")
                SelectedViewModel = new WaitingCallViewModel(ViewModelNavigator, parameters);
            if (obj.ToString() == "OnGoingCallViewModel")
                SelectedViewModel = new OnGoingCallViewModel(ViewModelNavigator, parameters);
        }

        public void ShowVoiceCall()
        {
            if (flagShowVoiceCall == 0)
            {
                //check if user has chosen room
                if (currentRoom.ID == null)
                {
                    new MessageDialog("Empty Room", "Please choose the room you want to call").ShowDialog();
                    return;
                }

                var callingWindow = new CallingWindow(currentRoom);
                callingWindow.Show();
                flagShowVoiceCall = 1;

                //emit to server
                var data = new JObject();
                data.Add("room_id", currentRoom.ID);
                data.Add("room", JObject.FromObject(currentRoom));
                socket.Emit("new_call", data);
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                    if (window.Title == "CallingWindow")
                        window.Close();
                flagShowVoiceCall = 0;
            }
        }

        public void OnRoomChange(RoomModel room)
        {
            currentRoom = room;
            callingModel.Room = room;
        }

        private void HandleSocket()
        {
            socket.On("new_call", arg =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    //check if user is having a call
                    if (callingModel.State == CallingState.BUSY)
                        return;

                    var data = (JObject) arg;
                    var roomId = data.GetValue("room_id").ToString();
                    var room = data.GetValue("room").ToObject<RoomModel>();
                    callingModel.State = CallingState.BUSY;

                    var receiveCallWindow = new ReceiveCallWindow(room);
                    receiveCallWindow.Show();
                });
            });

            //socket.On("accept_call", (arg) =>
            //{
            //    Application.Current.Dispatcher.Invoke((Action)delegate
            //    {
            //        callingModel.SendVoice();
            //    });
            //});
        }

        private void OnLoad()
        {
            //Debug.LogOutput("On load command activated-------------");
            //callingModel.Receive();
        }
    }
}