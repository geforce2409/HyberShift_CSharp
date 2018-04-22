using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HyberShift_CSharp.Model
{
    public class ListOnline
    {
        private static ListOnline instance = null;
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
            this.list.Clear();
            this.list = listSource;
        }

        public void addUserOnline(UserOnline newUser)
        {
            //check if already in list
            for (int i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).Email.Equals(newUser.Email))
                    return;

            list.Add(newUser);
        }

        public void removeUserOnline(String name)
        {
            if (list.Count() <= 0) return;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).Name.Equals(name))
                {
                    list.RemoveAt(i);
                }
            }
        }

        public ArrayList getListName()
        {
            ArrayList listName = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
                listName.Add(list.ElementAt(i).Name);
            return listName;
        }

        public ArrayList getListEmail()
        {
            ArrayList listEmail = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
                listEmail.Add(list.ElementAt(i).Email);
            return listEmail;
        }
    }

}
