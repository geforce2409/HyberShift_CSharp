using HyberShift_CSharp.View.SignIn;

namespace HyberShift_CSharp.Model
{
    internal class MainModel
    {
        // constructor
        public MainModel()
        {

        }

        // getter and setter

        public void SignOut()
        {
            SignInPage signInPage = new SignInPage();
            signInPage.Show();
            CloseWindowManager.CloseMainWindow();
        }
    }
}