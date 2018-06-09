using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using HyberShift_CSharp.Utilities;

namespace HyberShift_CSharp.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _stateClosed = true;

        public MainWindow()
        {
            InitializeComponent();
            //var socketAPI = SocketAPI.GetInstance();
            //socketAPI.Connect();
        }

        private void ButtonMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (_stateClosed)
            {
                var sb = FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                var sb = FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            _stateClosed = !_stateClosed;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            var socket = SocketAPI.GetInstance().GetSocket();
            var socketAPI = SocketAPI.GetInstance();
            socketAPI.Disconnect();
            base.OnClosing(e);
            Application.Current.Shutdown();
        }
    }
}