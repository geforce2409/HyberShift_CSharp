using System.Security;
using System.Windows.Controls;

namespace HyberShift_CSharp.View.SignIn
{
    /// <summary>
    ///     Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInControl : UserControl, IHavePassword
    {
        public SignInControl()
        {
            InitializeComponent();
            //DataContext = new LoginViewModel();
        }

        public SecureString Password => FloatingPasswordBox.SecurePassword;

        public SecureString ConfirmPassword { get; }
    }
}