using HyberShift_CSharp.Model;
using HyberShift_CSharp.View;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    internal class CreateRoomViewModel : BaseViewModel
    {
        private readonly Room room;
        private readonly CreateRoomModel createRoomModel;

        // constructor
        public CreateRoomViewModel()
        {
            createRoomModel = new CreateRoomModel();
            room = new Room();
            CreateRoomCommand = new DelegateCommand(CreateRoom);
        }

        // getter and setter
        public DelegateCommand CreateRoomCommand { get; set; }

        //public string ID
        //{
        //    get { return room.ID; }
        //    set { room.ID = value; NotifyChanged("ID"); }
        //}
        public string RoomName
        {
            get => room.Name;
            set
            {
                room.Name = value;
                NotifyChanged("RoomName");
            }
        }

        public string Email { get; set; }
        //public string Email
        //{
        //    // TO-DO: Lấy từ list members của room ghép lại thành chuỗi
        //    get => room.Members.ToString();

        //    // TO-DO: Tách từ input string (value) được ngăn cách bởi dấu ; thành mảng các members rồi gán cho room
        //    set => NotifyChanged("Email");
        //}

        // method or command
        // ...
        public void CreateRoom()
        {           
            createRoomModel.CreateRoom();
            //NotifyChanged("attributeX");  // this will automatically update attributeX  
        }

    }
}