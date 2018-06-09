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
        }

        // getter and setter
        public DelegateCommand SignOutCommand { get; set; }

        public void SignOut()
        {
            // TO-DO: Xử lý quay lại màn hình Login
            mainModel.SignOut();
        }
    }
}