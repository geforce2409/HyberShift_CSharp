using System.Collections.ObjectModel;
using HyberShift_CSharp.Model;

namespace HyberShift_CSharp.ViewModel
{
    public class RoomViewModel : BaseViewModel
    {
        private readonly RoomModel roomModel;

        // constructor
        public RoomViewModel()
        {
            roomModel = new RoomModel();
            Name = "";
            Members = "";
        }

        public RoomViewModel(string id, string name, ObservableCollection<string> members)
        {
            roomModel = new RoomModel(id, name, members);
        }

        // getter and setter
        public string ID
        {
            get => roomModel.ID;
            set
            {
                roomModel.ID = value;
                NotifyChanged("ID");
            }
        }

        public string Name
        {
            get => roomModel.Name;
            set
            {
                if (Name == null)
                    roomModel.Name = "Empty Name";
                else
                    roomModel.Name = value;
                NotifyChanged("Name");
            }
        }

        public string Members
        {
            get
            {
                var temp = "";
                foreach (var mem in roomModel.Members) temp += mem + " ";
                return temp;
            }
            set { }
        }
    }
}