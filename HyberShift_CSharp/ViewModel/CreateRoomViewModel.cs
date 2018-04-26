using HyberShift_CSharp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    class CreateRoomViewModel: BaseViewModel
    {
        Room room;

        // getter and setter
        //public string ID
        //{
        //    get { return room.ID; }
        //    set { room.ID = value; NotifyChanged("ID"); }
        //}
        public string TxtRoomName
        {
            get { return room.Name; }
            set { room.Name = value; NotifyChanged("TxtRoomName"); }
        }
        public string TxtRoomMember
        {
            get
            {
                // TO-DO: Lấy từ list members của room ghép lại thành chuỗi

                return null;    // temp
            }
            set
            {
                // TO-DO: Tách từ input string (value) được ngăn cách bởi dấu ; thành mảng các members rồi gán cho room
                NotifyChanged("TxtRoomMember");
            }
        }

        // constructor
        public CreateRoomViewModel(): base()
        {
            room = new Room();
        }

        // method or command
        // ...
    }
}
