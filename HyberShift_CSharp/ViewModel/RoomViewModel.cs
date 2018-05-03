using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyberShift_CSharp.Model;

namespace HyberShift_CSharp.ViewModel
{
    class RoomViewModel : BaseViewModel
    {
        private RoomModel roomModel;
        // constructor
        public RoomViewModel()
        {
            roomModel = new RoomModel();
            Name = "AAAAAAAAAAA";
            Member = "BBBBBBB, CCCCCCCC";
        }

        // getter and setter
        public string Name
        {
            get { return roomModel.Name; }
            set
            {
                if (Name == null)
                    roomModel.Name = "Empty Name";
                else
                    roomModel.Name = value;
                //NotifyChanged("Name");
            }
        }

        public string Member { get; set; }

        // methods
        public void SetInfo(RoomModel room)
        {
            Name = room.Name;
            Member = room.Members.ElementAt(0);
            for (int i = 1; i < room.Members.Count; i++)
                Member += ", " + room.Members.ElementAt(i);
        }
    }
}
