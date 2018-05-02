using System;
using System.Collections.Generic;
using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.View;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    internal class CreateRoomViewModel : BaseViewModel
    {
        private readonly Room room;
        private readonly CreateRoomModel createRoomModel;
        private string[] separators = { ",", "!", "?", ";", ":", " " };
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
            get
            {
                return createRoomModel.InputRoomName;
                //room.Name;
            } 
            set
            {
                createRoomModel.InputRoomName = value;
                //room.Name = value;
                NotifyChanged("RoomName");
            }
        }

        public string Email { get; set; }

        //public string Email
        //{
        //    // TO-DO: Lấy từ list members của room ghép lại thành chuỗi
        //    get => createRoomModel.InputRoomName;
        //    //get => createRoomModel.InputEmailMember;
        //    //return room.Members.ToString();

        //    // TO-DO: Tách từ input string (value) được ngăn cách bởi dấu ; thành mảng các members rồi gán cho room           
        //    set
        //    {
        //        createRoomModel.InputEmailMember = Email.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //        //for (int i = 0; i < createRoomModel.InputEmailMember.Length; i++)
        //        //    createRoomModel.InputEmailMember[i] = value;
        //        NotifyChanged("Email");
        //    }
        //}

        // method or command
    // ...
    public void CreateRoom()
        {
            createRoomModel.InputEmailMember = Email.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (createRoomModel.IsValidCreateRoom())
                createRoomModel.CreateRoom();
            else
            {
                MessageBox.Show("Something is wrong. Please try again", "Create room failed",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //NotifyChanged("attributeX");  // this will automatically update attributeX  
        }

    }
}