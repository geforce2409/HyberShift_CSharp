using System;
using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Model
{
    public class TaskModel
    {
        private readonly Socket socket = SocketAPI.GetInstance().GetSocket();

        public TaskModel()
        {
        }

        public TaskModel(string id, string name, string des, DateTime startday, DateTime endday, string performer,
            double progress, TaskType tag) : this()
        {
            ID = id;
            Name = name;
            Description = des;
            StartDay = startday;
            EndDay = endday;
            Performer = performer;
            Progress = progress;
            SliderProgress = Progress;
            Tag = tag;
        }

        //getter and setter
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Performer { get; set; }
        public double Progress { get; set; } // 0 to 1 --> 0% to 100%
        public TaskType Tag { get; set; }

        public string TaskIcon
        {
            get
            {
                switch (Tag)
                {
                    case TaskType.TO_DO:
                        return "PlaylistCheck";
                    case TaskType.IN_PROGRESS:
                        return "ChartDonut";
                    case TaskType.DONE:
                        return "Check";
                    case TaskType.WARNING:
                        return "Alert";
                    case TaskType.DELAY:
                        return "ClockAlert";
                    case TaskType.BACKLOG:
                        return "Database";
                    default:
                        return string.Empty;
                }
            }
        }

        public string ProgressText => Math.Round(Progress, 2) * 100 + " %";

        public double SliderProgress { get; set; }

        public void EmitToServer(string roomId)
        {
            // check data
            if (Description == string.Empty || Description == null)
                Description = "No description";

            var data = new JObject();
            data.Add("room_id", roomId);
            data.Add("work", Name);
            data.Add("description", Description);
            data.Add("performer", Performer);
            data.Add("start_day", StartDay);
            data.Add("end_day", EndDay);
            data.Add("tag", Tag.ToString());
            data.Add("progress", Progress);

            socket.Emit("create_task", data);
        }
    }
}