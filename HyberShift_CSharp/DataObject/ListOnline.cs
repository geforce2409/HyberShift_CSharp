using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HyberShift_CSharp.Model
{
    public class ListOnline
    {
        private static ListOnline instance;
        private List<UserOnline> list;

        public ListOnline()
        {
            list = new List<UserOnline>();
        }

        public static ListOnline getInstance()
        {
            if (instance == null)
                instance = new ListOnline();
            return instance;
        }

        public void setListOnline(List<UserOnline> listSource)
        {
            list.Clear();
            list = listSource;
        }

        public void addUserOnline(UserOnline newUser)
        {
            //check if already in list
            for (var i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).Email.Equals(newUser.Email))
                    return;

            list.Add(newUser);
        }

        public void removeUserOnline(string name)
        {
            if (list.Count() <= 0) return;
            for (var i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).Name.Equals(name))
                    list.RemoveAt(i);
        }

        public ArrayList getListName()
        {
            var listName = new ArrayList();
            for (var i = 0; i < list.Count(); i++)
                listName.Add(list.ElementAt(i).Name);
            return listName;
        }

        public ArrayList getListEmail()
        {
            var listEmail = new ArrayList();
            for (var i = 0; i < list.Count(); i++)
                listEmail.Add(list.ElementAt(i).Email);
            return listEmail;
        }
    }
}