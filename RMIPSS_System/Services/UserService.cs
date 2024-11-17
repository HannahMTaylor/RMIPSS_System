namespace RMIPSS_System.Services;

public class UserService
{
    public bool IsPasswordSame(string password1, string password2)
    {
        return password1.Equals(password2);
    }
}
