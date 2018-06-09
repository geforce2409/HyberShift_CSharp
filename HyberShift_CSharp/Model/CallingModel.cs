using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Utilities;

namespace HyberShift_CSharp.Model
{
    public class CallingModel
    {
        private static CallingModel instance;
        private readonly VoiceAPI voiceAPI;

        public CallingModel()
        {
            //State = CallingState.FREE;
            Room = new RoomModel();
            voiceAPI = VoiceAPI.GetInstance();
        }

        public CallingModel(RoomModel room) : this()
        {
            Room = room;
        }

        public CallingState State { get; set; }
        public RoomModel Room { get; set; }

        public static CallingModel GetInstace()
        {
            if (instance == null)
                instance = new CallingModel();
            return instance;
        }

        public static CallingModel GetInstace(RoomModel room)
        {
            if (instance == null)
                instance = new CallingModel(room);
            instance.Room = room;
            return instance;
        }

        public void SendVoice()
        {
            voiceAPI.Send(Room.ID);
        }

        public void StopSending()
        {
            voiceAPI.StopSending();
        }

        public void Receive()
        {
            voiceAPI.Receive();
        }

        public void StopReceiving()
        {
            voiceAPI.StopReceiving();
        }
    }
}