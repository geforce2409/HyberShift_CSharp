using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HyberShift_CSharp.Model
{
    public class ListRoom
    {
        private static ListRoom instance;
        private List<Room> list;

        public ListRoom()
        {
            list = new List<Room>();
        }

        public static ListRoom getInstance()
        {
            if (instance == null)
                instance = new ListRoom();
            return instance;
        }

        public List<Room> getListRoom()
        {
            return this.list;
        }

        public ArrayList getListRoomName()
        {
            ArrayList lst = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                lst.Add(list.ElementAt(i).Name);
            }

            return lst;
        }

        public ArrayList getMembersFrom(String roomName)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).Name.Equals(roomName))
                {
                    return list.ElementAt(i).Members;
                }
            }

            return new ArrayList();
        }

        public Room getRoomFromName(String roomName)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                Room tempRoom = list.ElementAt(i);
                if (tempRoom.Name.Equals(roomName))
                    return tempRoom;
            }
            return null;
        }

        public Room getRoomById(String id)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                Room tempRoom = list.ElementAt(i);
                if (tempRoom.ID.Equals(id))
                    return tempRoom;
            }
            return null;
        }

        public int getIndexOfRoom(String id)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                Room tempRoom = list.ElementAt(i);
                if (tempRoom.ID.Equals(id))
                    return i;
            }
            return -1;
        }

        public ArrayList getMembersFrom(int indexRoom)
        {
            if (indexRoom >= list.Count())
                return new ArrayList();

            return list.ElementAt(indexRoom).Members;
        }

        public void addRoom(Room room)
        {
            //Kiểm tra trÃ¹ng trÆ°á»›c khi add
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).Name.Equals(room.Name))
                    return;
            }
            list.Add(room);
        }

        //public ObservableList<String> getOListRoomName()
        //{
        //    ObservableList<String> olist = FXCollections.observableArrayList(this.getListRoomName());
        //    return olist;
        //}

        //public ObservableList<Room> getOList()
        //{
        //    return FXCollections.observableArrayList(this.list);
        //}

        public void setNewMessageAtRoom(String roomId, bool value)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).ID.Equals(roomId))
                {
                    list.ElementAt(i).HasNewMessage.Equals(value);
                }
            }
        }

    }

}
