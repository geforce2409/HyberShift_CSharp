namespace HyberShift_CSharp.Model
{
    public class UserOnline
    {
        public UserOnline()
        {
        }

        public UserOnline(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}