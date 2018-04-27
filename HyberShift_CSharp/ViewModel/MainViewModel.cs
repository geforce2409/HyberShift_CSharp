using System;
using HyberShift_CSharp.Model;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly MainModel mainModel;

        // constructor
        public MainViewModel()
        {
            mainModel = new MainModel();
            SignOutCommand = new DelegateCommand(SignOut);
            ViewID = Guid.NewGuid();
        }

        // getter and setter
        public DelegateCommand SignOutCommand { get; set; }

        public Guid ViewID { get; }

        public void SignOut()
        {
            // TO-DO: Xử lý quay lại màn hình Login
            //CloseWindowManager.CloseMainWindow(ViewID);
        }
    }
}