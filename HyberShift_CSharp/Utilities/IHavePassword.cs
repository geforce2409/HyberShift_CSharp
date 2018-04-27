using System.Security;

namespace HyberShift_CSharp
{
    public interface IHavePassword
    {
        SecureString Password { get; }
        SecureString ConfirmPassword { get; }
    }
}