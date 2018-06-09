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
    public class MakingCallViewModel : BaseViewModel
    {
        private readonly Action<object, object[]> navigate;
        private readonly CallingModel callingModel;
        private readonly RoomModel currentRoom;
        private readonly Socket socket;


        public MakingCallViewModel()
        {
            socket = SocketAPI.GetInstance().GetSocket();
            currentRoom = new RoomModel();
            callingModel = CallingModel.GetInstace();
            HangupCommand = new DelegateCommand(Hangup);
            HandleSocket();
        }

        public MakingCallViewModel(Action<object, object[]> navigate, params object[] parameters) : this()
        {
            this.navigate = navigate;
            currentRoom = (RoomModel) parameters[0];
        }

        public DelegateCommand HangupCommand { get; set; }

        private void HandleSocket()
        {
            socket.On("accept_call", arg =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    if (callingModel.State == CallingState.BUSY)
                        return;

                    var data = (JObject) arg;
                    callingModel.State = CallingState.BUSY;
                    navigate.Invoke("OnGoingCallViewModel", new object[] {currentRoom});
                });
            });
        }

        private void Hangup()
        {
            //emit end call to server
            socket.Emit("end_call", currentRoom.ID);
            callingModel.State = CallingState.FREE;

            foreach (Window window in Application.Current.Windows)
                if (window.Title == "CallingWindow")
                    window.Close();
        }
    }
}