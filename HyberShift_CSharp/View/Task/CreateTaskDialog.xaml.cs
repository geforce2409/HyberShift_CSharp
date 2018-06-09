using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.ViewModel;

namespace HyberShift_CSharp.View.Task
{
    /// <summary>
    ///     Interaction logic for CreateTaskDialog.xaml
    /// </summary>
    public partial class CreateTaskDialog : Window
    {
        public CreateTaskDialog()
        {
            InitializeComponent();
        }

        public CreateTaskDialog(RoomModel currentRoom) : this()
        {
            ((CreateTaskViewModel) DataContext).CurrentRoom = currentRoom;
            ((CreateTaskViewModel) DataContext).ListMembers = currentRoom.Members;
        }
    }
}