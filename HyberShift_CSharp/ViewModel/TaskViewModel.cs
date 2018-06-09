using System;
using System.Collections.ObjectModel;
using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.Enum;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View.Task;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.ViewModel
{
    public class TaskViewModel : BaseViewModel
    {
        private RoomModel currentRoom;
        private readonly ListTaskModel listTaskModel;
        private readonly Socket socket;


        public TaskViewModel()
        {
            socket = SocketAPI.GetInstance().GetSocket();
            listTaskModel = ListTaskModel.GetInstance();
            currentRoom = new RoomModel();

            CreateTaskCommand = new DelegateCommand(CreateTask);
            RoomChangeCommand = new DelegateCommand<RoomModel>(OnRoomChange);
            UpdateProgressCommand = new DelegateCommand(UpdateTask);

            ////test
            //ListTask.Add(new TaskModel("1", "Name 1", "Des 1", DateTime.Now, DateTime.Now, "Per 1", 0.2, Model.Enum.TaskType.TO_DO));
            //ListTask.Add(new TaskModel("2", "Name 2", "Des 2", DateTime.Now, DateTime.Now, "Per 2", 0.5, Model.Enum.TaskType.BACKLOG));
            //ListTask.Add(new TaskModel("3", "Name 3", "Des 3", DateTime.Now, DateTime.Now, "Per 3", 0.7, Model.Enum.TaskType.WARNING));
            //ListTask.Add(new TaskModel("4", "Name 4", "Des 4", DateTime.Now, DateTime.Now, "Per 4", 1, Model.Enum.TaskType.DELAY));

            HandleSocket();
        }

        // getter and setter
        public DelegateCommand CreateTaskCommand { get; set; }
        public DelegateCommand<RoomModel> RoomChangeCommand { get; set; }
        public DelegateCommand UpdateProgressCommand { get; set; }
        public TaskModel SelectedTask { get; set; }

        public ObservableCollection<TaskModel> ListTask
        {
            get => listTaskModel.List;
            set
            {
                listTaskModel.List = value;
                NotifyChanged("ListTask");
            }
        }

        private void HandleSocket()
        {
            // Handle socket
            socket.On("task_change", arg =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var data = (JObject) arg;
                    var roomid = data.GetValue("room_id").ToString();
                    if (!currentRoom.ID.Equals(roomid))
                        return;

                    var id = data.GetValue("task_id").ToString();
                    var name = data.GetValue("work").ToString();
                    var description = data.GetValue("description").ToString();
                    var performer = data.GetValue("performer").ToString();
                    var startday = (DateTime) data.GetValue("start_day");
                    var endday = (DateTime) data.GetValue("end_day");
                    var tag = TaskTypeModel.GetTaskType(data.GetValue("tag").ToString());
                    var progress = (double) data.GetValue("progress");

                    listTaskModel.AddWithCheck(
                        new TaskModel(id, name, description, startday, endday, performer, progress, tag), "ID");
                    NotifyChanged("ListTask");
                });
            });
        }

        private void CreateTask()
        {
            var createTaskDialog = new CreateTaskDialog(currentRoom);
            createTaskDialog.ShowDialog();
        }

        private void OnRoomChange(RoomModel obj)
        {
            currentRoom = obj;
        }

        private void UpdateTask()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Debug.LogOutput(SelectedTask.Progress.ToString());
                Debug.LogOutput(currentRoom.ID);
                Debug.LogOutput(SelectedTask.ID);
                var data = new JObject();
                data.Add("room_id", currentRoom.ID);
                data.Add("task_id", SelectedTask.ID);
                var dataUpdate = new JObject();
                dataUpdate.Add("progress", SelectedTask.SliderProgress);
                if (SelectedTask.SliderProgress > 0 && SelectedTask.SliderProgress < 1)
                    dataUpdate.Add("tag", TaskType.IN_PROGRESS.ToString());
                else if (SelectedTask.SliderProgress == 1)
                    dataUpdate.Add("tag", TaskType.DONE.ToString());
                data.Add("update", dataUpdate);

                socket.Emit("task_modify", data);

                ListTask.Clear();
            });
        }
    }
}