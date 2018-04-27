using HyberShift_CSharp.Model;

namespace HyberShift_CSharp.ViewModel
{
    internal class CreateRoomViewModel : BaseViewModel
    {
        private readonly Room room;

        // constructor
        public CreateRoomViewModel()
        {
            room = new Room();
        }

        // getter and setter
        //public string ID
        //{
        //    get { return room.ID; }
        //    set { room.ID = value; NotifyChanged("ID"); }
        //}
        public string TxtRoomName
        {
            get => room.Name;
            set
            {
                room.Name = value;
                NotifyChanged("TxtRoomName");
            }
        }

        public string TxtRoomMember
        {
            get => null;
            set => NotifyChanged("TxtRoomMember");
        }

        // method or command
        // ...
    }
}