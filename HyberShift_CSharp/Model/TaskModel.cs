﻿using HyberShift_CSharp.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class TaskModel
    {
        public TaskModel()
        {
           
        }

        public TaskModel(string id, string name, string des, DateTime startday, DateTime endday, string performer, double progress, TaskType tag)
        {
            ID = id;
            Name = name;
            Description = des;
            StartDay = startday;
            EndDay = endday;
            Performer = performer;
            Progress = progress;
            Tag = tag;
        }

        //getter and setter
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Performer { get; set; }
        public double Progress { get; set; }    // 0 to 1 --> 0% to 100%
        public TaskType Tag { get; set; }
    }
}
