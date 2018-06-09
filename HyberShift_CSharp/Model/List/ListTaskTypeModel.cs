namespace HyberShift_CSharp.Model.List
{
    internal class ListTaskTypeModel : BaseList<TaskTypeModel>
    {
        public ListTaskTypeModel()
        {
            List.Clear();
            Add(new TaskTypeModel("PlaylistCheck", "TO DO"));
            Add(new TaskTypeModel("ChartDonut", "IN PROGRESS"));
            Add(new TaskTypeModel("Alert", "WARNING"));
            Add(new TaskTypeModel("Database", "BACKLOG"));
        }
    }
}