using System.Text.RegularExpressions;

namespace TaskTracker.Validation.UserValidation
{
    public class UserValidator
    {
        public static bool ValidateLogin(string login)
        {
            Regex rx = new Regex("^[a-zA-Z0-9]+$");
            return rx.Match(login).Success;
        }
        public static bool ValidatePassword(string password)
        {
            Regex rx = new Regex("^[a-zA-Z0-9]+$");
            return rx.Match(password).Success;
        }
    }
}
