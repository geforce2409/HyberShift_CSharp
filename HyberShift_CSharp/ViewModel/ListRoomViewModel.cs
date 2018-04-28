using System;
using System.Collections.ObjectModel;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;

namespace HyberShift_CSharp.ViewModel
{
    public class ListRoomViewModel : BaseViewModel
    {
        private readonly ListRoomModel roomModel;

        // constructor
        public ListRoomViewModel()
        {
            roomModel = new ListRoomModel();
        }

        // getter and setter
        public ObservableCollection<Room> List
        {
            get => roomModel.List;
            set
            {
                roomModel.List = value;
                NotifyChanged("List");
            }
        }

        // method
 
    }
}