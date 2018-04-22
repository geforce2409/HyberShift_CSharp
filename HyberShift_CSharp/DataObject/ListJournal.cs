using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HyberShift_CSharp.Model
{
    public class ListJournal
    {
        private static ListJournal instance;
        private readonly List<Journal> list;

        public ListJournal()
        {
            list = new List<Journal>();
        }

        public static ListJournal getInstance()
        {
            if (instance == null)
                instance = new ListJournal();

            return instance;
        }

        public List<Journal> getList()
        {
            return list;
        }

        public ArrayList getListWork()
        {
            var result = new ArrayList();
            for (var i = 0; i < list.Count(); i++) result.Add(list.ElementAt(i).Work);

            return result;
        }

        public void addJournal(Journal journal)
        {
            //check
            for (var i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).Equals(journal))
                    return;

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