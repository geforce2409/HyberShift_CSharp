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

            HandleSocket();
        }

        private void HandleSocket()
        {
            // Handle socket
            
        }
    }
}
