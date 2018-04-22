using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HyberShift_CSharp.Model
{
    public class Room
    {
        private String id;
        private String name;
        private ArrayList members;  //list of name of users in room
        private bool hasNewMessage;

        public Room()
        {
            members = new ArrayList();
            //hasNewMessage = false;
        }

        public Room(String id, String name, ArrayList members)
        {
            this.id = id;
            this.name = name;
            this.members = members;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public ArrayList Members
        {
            get { return members; }
            set { members = value; }
        }

        public bool HasNewMessage
        {
            get { return hasNewMessage; }
            set { hasNewMessage = value; }
        }

        public void AddMemebers(String member) { members.Add(member); }
        public int GetMemebersCount() { return members.Count; }

    }

}
