using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HyberShift_CSharp.View;
using HyberShift_CSharp.View.Calling;
using MaterialDesignThemes.Wpf;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class CallingViewModel: BaseViewModel
    {
        private int flagShowVoiceCall = 0; 
        private object selectedViewModel;
        public DelegateCommand ShowVoiceCallCommand { get; set; }
        public CallingViewModel()
        {
            SelectedViewModel = new WaitingCallViewModel(ViewModelNavigator);
            ShowVoiceCallCommand = new DelegateCommand(ShowVoiceCall);
        }

        public object SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                NotifyChanged("SelectedViewModel");
            }
        }

        public void ViewModelNavigator(object obj)
        {
            if (obj.ToString() == "WaitingCallViewModel") SelectedViewModel = new WaitingCallViewModel(ViewModelNavigator);
            if (obj.ToString() == "OnGoingCallViewModel") SelectedViewModel = new OnGoingCallViewModel(ViewModelNavigator);
        }

        public void ShowVoiceCall()
        {
            if (flagShowVoiceCall == 0)
            {
                CallingWindow callingWindow = new CallingWindow();
                callingWindow.Show();
                flagShowVoiceCall = 1;
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                    if (window.Title == "CallingWindow")
                        window.Close();
                flagShowVoiceCall = 0;
            }
        }
    }
}
