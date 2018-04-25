using System.Windows.Controls;
using HyberShift_CSharp.ViewModel;
using Newtonsoft.Json;

namespace HyberShift_CSharp.View.SignIn
{
    /// <summary>
    /// Interaction logic for SignUpControl.xaml
    /// </summary>
    public partial class SignUpControl : UserControl, IHavePassword
    {
        public SignUpControl()
        {
            InitializeComponent();
        }

        public System.Security.SecureString Password
        {
            get
            {
                return FloatingPasswordBox.SecurePassword;
            }
        }

        public System.Security.SecureString ConfirmPassword
        {
            get
            {
                return FloatingConfirmPasswordBox.SecurePassword;
            }
        }
    }
}
