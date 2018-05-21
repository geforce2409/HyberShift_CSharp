using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    public class TaskViewModel: BaseViewModel
    {
        private Socket socket;
        private ListTaskModel listTaskModel;

        public ObservableCollection<TaskModel> ListTask
        {
            get { return listTaskModel.List; }
            set
            {
                listTaskModel.List = value;
                NotifyChanged("ListTask");
            }
        }

        
        public TaskViewModel():base()
        {
            socket = SocketAPI.GetInstance().GetSocket();
            listTaskModel = new ListTaskModel();

            //test
            ListTask.Add(new TaskModel("1", "Name 1", "Des 1", DateTime.Now, DateTime.Now, "Per 1", 0.2, Model.Enum.TaskType.TO_DO));
            ListTask.Add(new TaskModel("2", "Name 2", "Des 2", DateTime.Now, DateTime.Now, "Per 2", 0.5, Model.Enum.TaskType.BACKLOG));
            ListTask.Add(new TaskModel("3", "Name 3", "Des 3", DateTime.Now, DateTime.Now, "Per 3", 0.7, Model.Enum.TaskType.WARNING));
            ListTask.Add(new TaskModel("4", "Name 4", "Des 4", DateTime.Now, DateTime.Now, "Per 4", 1, Model.Enum.TaskType.DELAY));

            HandleSocket();
        }

        private void HandleSocket()
        {
            // Handle socket
            
        }
    }
}
