using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    public class OnGoingCallViewModel: BaseViewModel
    {
        private readonly Action<object> navigate;
        public OnGoingCallViewModel(): base()
        {

        }

        public OnGoingCallViewModel(Action<object> navigate)
        {
            this.navigate = navigate;
        }
    }
}
