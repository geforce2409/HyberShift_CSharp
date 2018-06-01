using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HyberShift_CSharp.ViewModel
{
    public class MakingCallViewModel: BaseViewModel
    {
        private readonly Action<object, object[]> navigate;
        public DelegateCommand HangupCommand { get; set; }


        public MakingCallViewModel(): base()
        {
            HangupCommand = new DelegateCommand(Hangup);
        }

        public MakingCallViewModel(Action<object, object[]> navigate, params object[] parameters): this()
        {
            this.navigate = navigate;
        }

        private void Hangup()
        {
            foreach (Window window in Application.Current.Windows)
                if (window.Title == "CallingWindow")
                    window.Close();
        }
    }
}
