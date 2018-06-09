using System.Windows;

namespace HyberShift_CSharp.View.Dialog
{
    /// <summary>
    ///     Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        public MessageDialog(string title, string content) : this()
        {
            tbTitle.Text = title;
            tbContent.Text = content;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
                if (window.Title == "MessageDialog")
                    window.Close();
        }
    }
}