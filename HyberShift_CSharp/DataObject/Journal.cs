using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HyberShift_CSharp.Model
{
    // class chứa thông tin journal của 1 project

    public class Journal
    {
        private String id;
        private String work;
        private List<String> listPerformer;    // nhá»¯ng ngÆ°á»�i thá»±c hiá»‡n (name)
        private bool isDone;                             // dành cho checkbox
        private String startDay;
        private String endDay;

        public Journal()
        {
            listPerformer = new List<String>();
        }

        public Journal(String id, String work, List<String> listPerformer, bool isDone, String startDay, String endDay)
        {
            listPerformer = new List<String>();
            this.id = id;
            this.work = work;
            this.listPerformer = listPerformer;
            this.isDone = isDone;
            this.startDay = startDay;
            this.endDay = endDay;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Work
        {
            get { return work; }
            set { work = value; }
        }

        public List<String> ListPerformer
        {
            get { return listPerformer; }
            set { listPerformer = value; }
        }

        public bool IsDone
        {
            get { return isDone; }
            set { isDone = value; }
        }

        public string StartDay
        {
            get { return startDay; }
            set { startDay = value; }
        }

        public string EndDay
        {
            get { return endDay; }
            set { endDay = value; }
        }

        public void addPerformer(String name)
        {

            //check
            for (int i = 0; i < listPerformer.Count(); i++)
            {
                if (listPerformer.ElementAt(i).Equals(name))
                    return;
            }

            listPerformer.Add(name);
        }
    }

}
