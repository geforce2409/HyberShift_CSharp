using System.Collections.ObjectModel;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;

namespace HyberShift_CSharp.ViewModel
{
    public class ListRoomViewModel : BaseViewModel
    {
        private readonly ListRoomModel model;

        // constructor
        public ListRoomViewModel()
        {
            model = new ListRoomModel();
        }

        // getter and setter
        public ObservableCollection<Room> List
        {
            get => model.List;
            set
            {
                model.List = value;
                NotifyChanged("List");
            }
        }

        // method
    }
}