using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model.List
{
    public class ListRoomModel: BaseList<Room>
    {
        private static ListRoomModel instance = null;
        // constructor
        public ListRoomModel(): base()
        {

        }

        // singleton method
        public static ListRoomModel GetInstance()
        {
            if (instance == null)
                instance = new ListRoomModel();
            return instance;
        }

        // getter and setter
        public ObservableCollection<object> IDList
        {
            get
            {
                return this.GetCollectionOfField("ID");
            }
        }

        public ObservableCollection<object> NameList
        {
            get
            {
                return this.GetCollectionOfField("Name");
            }
        }
        
        public ObservableCollection<object> MemberList
        {
            get
            {
                return this.GetCollectionOfField("Members");
            }
        }

        //method
        public Room GetRoomFromName(string roomName)
        {
            return this.GetFirstObjectByValue("Name", roomName);
        }

        public ObservableCollection<object> GetMembersFrom(string roomName)
        {
            return this.GetCollectionOfFieldByValue("Members", "Name", roomName);
        }

        public Room GetRoomById(string id)
        {
            return this.GetFirstObjectByValue("ID", id);
        }

        public int GetIndexOfRoom(string id)
        {
            return this.GetIndexByValue("ID", id);
        }

        public ObservableCollection<string> GetMembersFrom(int indexRoom)
        {
            return this.GetFieldValueByIndex("Members", indexRoom);
        }
    }
}
