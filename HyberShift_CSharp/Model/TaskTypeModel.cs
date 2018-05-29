using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class TaskTypeModel
    {
        public string Icon { get; set; }
        public string Content { get; set; }

        public TaskTypeModel()
        {

        }

        public TaskTypeModel(string icon, string content)
        {
            Icon = icon;
            Content = content;
        }
    }
}
