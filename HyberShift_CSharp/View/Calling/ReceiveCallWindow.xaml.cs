using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.ViewModel;

namespace HyberShift_CSharp.View.Calling
{
    /// <summary>
    ///     Interaction logic for ReceiveCallWindow.xaml
    /// </summary>
    public partial class ReceiveCallWindow : Window
    {
        public ReceiveCallWindow()
        {
            InitializeComponent();
        }

        public ReceiveCallWindow(RoomModel room) : this()
        {
            ((CallingViewModel) DataContext).CurrentRoom = room;
        }
    }
}