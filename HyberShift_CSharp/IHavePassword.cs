using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
