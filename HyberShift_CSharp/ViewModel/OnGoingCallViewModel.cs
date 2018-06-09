using System;
using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Utilities;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class OnGoingCallViewModel : BaseViewModel
    {
        private readonly Action<object, object[]> navigate;
        private readonly CallingModel callingModel;
        private readonly RoomModel currentRoom;

        public OnGoingCallViewModel()
        {
            currentRoom = new RoomModel();
            MuteCommand = new DelegateCommand(Mute);
            HangupCommand = new DelegateCommand(Exit);
            LoadCommand = new DelegateCommand(OnLoad);
        }

        public OnGoingCallViewModel(Action<object, object[]> navigate, params object[] parameters) : this()
        {
            this.navigate = navigate;
            currentRoom = (RoomModel) parameters[0];
            callingModel = CallingModel.GetInstace(currentRoom);
        }

        public DelegateCommand MuteCommand { get; set; }
        public DelegateCommand HangupCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }

        private void Mute()
        {
            Debug.LogOutput("Mute button clicked");

            // start the call
            callingModel.SendVoice();
        }

        private void Exit()
        {
            foreach (Window window in Application.Current.Windows)
                if (window.Title == "ReceiveCallWindow")
                    window.Close();

            callingModel.State = CallingState.FREE;
            callingModel.StopSending();
            callingModel.StopReceiving();
        }

        private void OnLoad()
        {
            Debug.LogOutput("On load command activated-------------");
            callingModel.Receive();
        }
    }
}