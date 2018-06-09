using HyberShift_CSharp.Model.Enum;

namespace HyberShift_CSharp.Model
{
    public class TaskTypeModel
    {
        public TaskTypeModel()
        {
        }

        public TaskTypeModel(string icon, string content)
        {
            Icon = icon;
            Content = content;
        }

        public string Icon { get; set; }
        public string Content { get; set; }

        public static TaskType GetTaskType(string type)
        {
            switch (type)
            {
                case "TO_DO":
                case "TO DO":
                    return TaskType.TO_DO;
                case "IN_PROGRESS":
                case "IN PROGRESS":
                    return TaskType.IN_PROGRESS;
                case "WARNING":
                    return TaskType.WARNING;
                case "BACKLOG":
                    return TaskType.BACKLOG;
                case "DONE":
                    return TaskType.DONE;
                case "DELAY":
                    return TaskType.DELAY;
                default:
                    return TaskType.BACKLOG;
            }
        }
    }
}