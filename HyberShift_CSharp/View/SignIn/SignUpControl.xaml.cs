using System.Security;
using System.Windows.Controls;
using HyberShift_CSharp.Utilities;

namespace HyberShift_CSharp.View.SignIn
{
    /// <summary>
    ///     Interaction logic for SignUpControl.xaml
    /// </summary>
    public partial class SignUpControl : UserControl, IHavePassword
    {
        public SignUpControl()
        {
            InitializeComponent();
        }

        public SecureString Password => FloatingPasswordBox.SecurePassword;

        public SecureString ConfirmPassword => FloatingConfirmPasswordBox.SecurePassword;
    }
}