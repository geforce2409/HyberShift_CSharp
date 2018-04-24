using System.Windows;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.ViewModel;

namespace HyberShift_CSharp.View.SignIn
{
    /// <summary>
    ///     Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInPage : Window
    {
        public SignInPage()
        {
            InitializeComponent();
            var socketAPI = SocketAPI.GetInstance();
            socketAPI.Connect();
        }
    }
}