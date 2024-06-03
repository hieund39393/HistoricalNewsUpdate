namespace HistoricalNewsUpdate.Models.User
{
    public class LoginResponse
    {
        public LoginResponse(string userName, string permissions)
        {
            UserName = userName;
            Permissions = permissions;
        }

        public string UserName { get; set; }
       
        public string Permissions { get; set; }

    }

}
