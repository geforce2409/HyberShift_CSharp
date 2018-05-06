using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.ViewModel
{
    public class ListRoomViewModel : BaseViewModel
    {
        private readonly ListRoomModel listRoomModel;
        private string selectedString;

        // constructor
        public ListRoomViewModel()
        {
            listRoomModel = new ListRoomModel();      
            ItemSelectedCommand = new DelegateCommand<RoomModel>(HandleSelectedItem);
        }

        // getter and setter
        public DelegateCommand<RoomModel> ItemSelectedCommand { get; set; } //Command use for SelectedChanged event of listview (or listbox, ...)

        public RoomModel ItemSelected // Property for binding when item was selected
        {
            get { return ItemSelected; }
            set
            {
                if (ItemSelected != null)
                {
                    ItemSelected = value;
                    RoomModel obj = value as RoomModel;
                    CurrentSelected = obj.Name;
                    NotifyChanged("CurrentSelected");
                }
            }
        }

        public string CurrentSelected
        {
            get { return selectedString; }
            set
            {
                if (value != selectedString)
                {
                    selectedString = value;
                    NotifyChanged("CurrentSelected");
                }
            }
        }

        public ObservableCollection<RoomModel> ListRoom
        {
            get { return listRoomModel.List; }
            set
            {
                listRoomModel.List = value;
                NotifyChanged("ListRoom");
            }
        }

        // method
        private void HandleSelectedItem(RoomModel obj)
        {
            CurrentSelected = obj.Name;
            Debug.LogOutput(CurrentSelected);
            ////get room by name
            //currRoom = listRoomModel.GetRoomById(ItemSelected.ID);
            //Debug.LogOutput("Current room: " + currRoom.Name);
            ////currRoom.setNewMessage(false);
            ////lvRoom.getItems().set(listRoomModel.GetIndexOfRoom(ItemSelected.ID), currRoom);       Chưa kiểu ý nghĩa lắm
            //listRoomModel.GetIndexOfRoom(ItemSelected.ID);
            ////System.out.println("Room id: " + currRoom.getId());

            ////emit to server to get message
            //socket.Emit("room_change", currRoom.ID);

            ////update UI room
            ////lblRoomName.setText(currRoom.getName());
        }
    }
}