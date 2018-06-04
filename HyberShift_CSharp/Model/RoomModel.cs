using System.Collections.ObjectModel;

namespace HyberShift_CSharp.Model
{
    public class RoomModel
    {
        public RoomModel()
        {
            Members = new ObservableCollection<string>();
            DisplayNewMessage = "Hidden";
            //hasNewMessage = false;
        }

        public RoomModel(string id, string name, ObservableCollection<string> members)
        {
            ID = id;
            Name = name;
            Members = members;
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string DisplayNewMessage { get; set; }

        public ObservableCollection<string> Members { get; set; }

        public string ListMembers
        {
            get
            {
                string temp = "";
                foreach(string mem in Members)
                {
                    temp += mem + " ";
                }
                return temp;
            }
        }

        public bool HasNewMessage { get; set; }

        public void AddMembers(string member)
        {
            Members.Add(member);
        }

        public int GetMemebersCount()
        {
            return Members.Count;
        }
    }
}