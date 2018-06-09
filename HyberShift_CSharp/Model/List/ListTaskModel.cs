using System.Collections.ObjectModel;
using HyberShift_CSharp.Model.Enum;

namespace HyberShift_CSharp.Model.List
{
    public class ListTaskModel : BaseList<TaskModel>
    {
        private static ListTaskModel instance;

        public ObservableCollection<object> ListTaskName => GetCollectionOfField("Name");

        public ObservableCollection<object> ListStartDay => GetCollectionOfField("StartDay");

        public ObservableCollection<object> ListEndDay => GetCollectionOfField("EndDay");

        public ObservableCollection<object> ListTag => GetCollectionOfField("Tag");

        public ObservableCollection<object> ListPerformer => GetCollectionOfField("Performer");

        public ObservableCollection<object> ListProgress => GetCollectionOfField("Progress");

        public static ListTaskModel GetInstance()
        {
            if (instance == null)
                instance = new ListTaskModel();
            return instance;
        }

        public TaskModel GetTaskFromID(string id)
        {
            return GetFirstObjectByValue("ID", id);
        }

        public TaskModel GetTaskFromName(string name)
        {
            return GetFirstObjectByValue("Name", name);
        }

        public ObservableCollection<TaskModel> GetTaskFromPerformer(string performer)
        {
            return GetCollectionByValue("Performer", performer);
        }

        public ObservableCollection<TaskModel> GetTaskFromProgress(string progress)
        {
            return GetCollectionByValue("Progress", progress);
        }

        public ObservableCollection<TaskModel> GetTaskFromTag(TaskType tag)
        {
            return GetCollectionByValue("Tag", tag);
        }

        public void Clear()
        {
            List.Clear();
        }
    }
}