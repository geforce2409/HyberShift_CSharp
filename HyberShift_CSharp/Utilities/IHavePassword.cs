using System.Security;

namespace HyberShift_CSharp.Utilities
{
    public interface IHavePassword
    {
        SecureString Password { get; }
        SecureString ConfirmPassword { get; }
    }
}