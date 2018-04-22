using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HyberShift_CSharp.Model
{
    public class ListJournal
    {
        private static ListJournal instance = null;
        List<Journal> list;

        public static ListJournal getInstance()
        {
            if (instance == null)
                instance = new ListJournal();

            return instance;
        }

        public ListJournal()
        {
            list = new List<Journal>();
        }

        public List<Journal> getList()
        {
            return list;
        }

        public ArrayList getListWork()
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                result.Add(list.ElementAt(i).Work);
            }

            return result;
        }

        public void addJournal(Journal journal)
        {
            //check
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).Equals(journal))
                    return;
            }

            list.Add(journal);
        }

        //public ObservableList<Journal> getOListJournal()
        //{
        //    ObservableList<Journal> olist = FXCollections.observableArrayList(this.list);
        //    return olist;
        //}

        //public ObservableList<String> getOListWork()
        //{
        //    ObservableList<String> olist = FXCollections.observableArrayList(this.getListWork());
        //    return olist;
        //}
    }

}
