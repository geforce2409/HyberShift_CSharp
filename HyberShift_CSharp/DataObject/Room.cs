using System.Collections;

namespace HyberShift_CSharp.Model
{
    public class Room
    {
        public Room()
        {
            Members = new ArrayList();
            //hasNewMessage = false;
        }

        public Room(string id, string name, ArrayList members)
        {
            ID = id;
            Name = name;
            Members = members;
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public ArrayList Members { get; set; }

        public bool HasNewMessage { get; set; }

        public void AddMemebers(string member)
        {
            Members.Add(member);
        }

        public int GetMemebersCount()
        {
            return Members.Count;
        }
    }
}