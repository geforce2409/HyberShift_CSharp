using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    public class ListRoomViewModel: BaseViewModel
    {
        private ListRoomModel model;

        // getter and setter
        public ObservableCollection<Room> List
        {
            get
            {
                return model.List;
            }
            set
            {
                model.List = value;
                NotifyChanged("List");
            }
        }

        // constructor
        public ListRoomViewModel() : base()
        {
            model = new ListRoomModel();
        }

        // method
    }
}
