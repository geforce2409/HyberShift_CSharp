using System;
using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.ViewModel
{
    public class WaitingCallViewModel : BaseViewModel
    {
        private readonly Action<object, object[]> navigate;
        private readonly RoomModel currentRoom;
        private readonly Socket socket;

        public WaitingCallViewModel()
        {
            socket = SocketAPI.GetInstance().GetSocket();
            currentRoom = new RoomModel();
            HangupCommand = new DelegateCommand(Exit);
            AcceptCommand = new DelegateCommand(AcceptCall);

            HandleSocket();
        }

        public WaitingCallViewModel(Action<object, object[]> navigate, params object[] parameters) : this()
        {
            this.navigate = navigate;

            currentRoom = (RoomModel) parameters[0];
        }

        public DelegateCommand HangupCommand { get; set; }
        public DelegateCommand AcceptCommand { get; set; }

        private void HandleSocket()
        {
            socket.On("end_call", () =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Exit();
                    CallingModel.GetInstace().State = CallingState.FREE;
                });
            });
        }

        private void Exit()
        {
            CallingModel.GetInstace().State = CallingState.FREE;

            foreach (Window window in Application.Current.Windows)
                if (window.Title == "ReceiveCallWindow")
                    window.Close();
        }

        private void AcceptCall()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                // emit to server accept call
                var data = new JObject();
                data.Add("room_id", currentRoom.ID);
                data.Add("user", JObject.FromObject(UserInfo.GetInstance()));
                socket.Emit("accept_call", data);

                CallingModel.GetInstace().State = CallingState.BUSY;
                navigate.Invoke("OnGoingCallViewModel", new object[] {currentRoom});
            });
        }
    }
}