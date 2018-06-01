using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class CallingModel
    {
        public CallingState State { get; set; }
        public RoomModel Room { get; set; }
        private VoiceAPI voiceAPI;

        public CallingModel()
        {
            State = CallingState.FREE;
            Room = new RoomModel();
            voiceAPI = VoiceAPI.GetInstance();
           
        }

        public CallingModel(RoomModel room): this()
        {
            Room = room;
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
