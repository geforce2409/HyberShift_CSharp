using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.ViewModel
{
    public class WaitingCallViewModel: BaseViewModel
    {
        private readonly Action<object, object[]> navigate;
        public WaitingCallViewModel(): base()
        {

        }

        public WaitingCallViewModel(Action<object, object[]> navigate, params object[] parameters): this()
        {
            this.navigate = navigate;
        }
    }
}
