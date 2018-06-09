using System;
using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View.Dialog;
using Newtonsoft.Json.Linq;

namespace HyberShift_CSharp.View
{
    /// <summary>
    ///     Interaction logic for AddMemberDialog.xaml
    /// </summary>
    public partial class AddMemberDialog : Window
    {
        private readonly RoomModel currentRoom;

        public AddMemberDialog()
        {
            InitializeComponent();
            currentRoom = new RoomModel();

            //SocketAPI.GetInstance().GetSocket().On("add_member_result", () =>
            //{
            //    tbNoice.Text = "Add member to room successfully!";
            //});
        }

        public AddMemberDialog(RoomModel room) : this()
        {
            currentRoom = room;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtMember.Text.Length == 0)
            {
                new MessageDialog("Empty member", "Insert at least one member").ShowDialog();
                return;
            }

            string[] separators = {",", "!", "?", ";", ":", " "};
            var members = txtMember.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var jarrayMember = new JArray();
            foreach (var mem in members) jarrayMember.Add(mem);

            var data = new JObject();
            data.Add("room_id", currentRoom.ID);
            data.Add("room_name", currentRoom.Name);
            data.Add("members", jarrayMember);

            SocketAPI.GetInstance().GetSocket().Emit("add_members", data);
        }
    }
}