using System.Collections.Generic;
using System.Linq;

namespace HyberShift_CSharp.Model
{
    // class chứa thông tin journal của 1 project

    public class Journal
    {
        public Journal()
        {
            ListPerformer = new List<string>();
        }

        public Journal(string id, string work, List<string> listPerformer, bool isDone, string startDay, string endDay)
        {
            listPerformer = new List<string>();
            ID = id;
            Work = work;
            ListPerformer = listPerformer;
            IsDone = isDone;
            StartDay = startDay;
            EndDay = endDay;
        }

        public string ID { get; set; }

        public string Work { get; set; }

        public List<string> ListPerformer { get; set; }

        public bool IsDone { get; set; }

        public string StartDay { get; set; }

        public string EndDay { get; set; }

        public void addPerformer(string name)
        {
            //check
            for (var i = 0; i < ListPerformer.Count(); i++)
                if (ListPerformer.ElementAt(i).Equals(name))
                    return;

            ListPerformer.Add(name);
        }
    }
}