using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using HyberShift_CSharp.Model;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly MainModel mainModel;
        private readonly Guid _viewId;

        // getter and setter
        public DelegateCommand SignOutCommand { get; set; }

        public Guid ViewID
        {
            get { return _viewId; }
        }

        // constructor
        public MainViewModel()
        {
            mainModel = new MainModel();
            SignOutCommand = new DelegateCommand(SignOut);
            _viewId = Guid.NewGuid();
        }

        public void SignOut()
        {
            // TO-DO: Xử lý quay lại màn hình Login
            //CloseWindowManager.CloseMainWindow(ViewID);
        }  
    }
}
