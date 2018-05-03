using System.Collections.ObjectModel;

namespace HyberShift_CSharp.Model
{
    public class RoomModel
    {
        public RoomModel()
        {
            Members = new ObservableCollection<string>();
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

        public ObservableCollection<string> Members { get; set; }

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