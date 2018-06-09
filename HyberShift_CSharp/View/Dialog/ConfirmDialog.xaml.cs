using System;
using System.Windows;

namespace HyberShift_CSharp.View.Dialog
{
    /// <summary>
    ///     Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        private readonly Action confirmAction;

        public ConfirmDialog()
        {
            InitializeComponent();
        }

        public ConfirmDialog(string title, string content, Action confirmAction) : this()
        {
            tbTitle.Text = title;
            tbContent.Text = content;
            this.confirmAction = confirmAction;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            confirmAction.Invoke();
            Exit();
        }

        private void Exit()
        {
            foreach (Window window in Application.Current.Windows)
                if (window.Title == "ConfirmDialog")
                    window.Close();
        }
    }
}