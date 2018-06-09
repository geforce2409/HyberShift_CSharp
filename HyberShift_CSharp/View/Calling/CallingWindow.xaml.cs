using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.ViewModel;

namespace HyberShift_CSharp.View.Calling
{
    /// <summary>
    ///     Interaction logic for CallingWindow.xaml
    /// </summary>
    public partial class CallingWindow : Window
    {
        public CallingWindow()
        {
            InitializeComponent();
        }

        public CallingWindow(RoomModel room) : this()
        {
            ((CallingViewModel) DataContext).CurrentRoom = room;
            //((CallingViewModel)DataContext).SelectedViewModel = new MakingCallViewModel(((CallingViewModel)DataContext).ViewModelNavigator, room);
            ((CallingViewModel) DataContext).SelectedViewModel =
                new OnGoingCallViewModel(((CallingViewModel) DataContext).ViewModelNavigator, room);
        }
    }
}