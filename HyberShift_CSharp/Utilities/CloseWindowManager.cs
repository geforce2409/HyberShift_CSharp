using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HyberShift_CSharp
{
    public static class WindowManager
    {
        public static void CloseLoginWindow(Guid id)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == "SignIn")
                    window.Close();
                //var w_id = window.DataContext as IRequireViewIdentification;
                //if (w_id != null && w_id.ViewID.Equals(id))
                //{
                //    window.Close();
                //}
            }
        }

        public static void CloseMainWindow(Guid id)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == "MainWindow")
                    window.Close();
            }
        }
    }
}
