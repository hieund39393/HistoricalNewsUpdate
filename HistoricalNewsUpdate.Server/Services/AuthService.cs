using Euroland.NetCore.ToolsFramework.Authentication;
using HistoricalNewsUpdate.Common.Exceptions;
using HistoricalNewsUpdate.Models.User;
using HistoricalNewsUpdate.Common;

namespace HistoricalNewsUpdate.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly ILdapAuthentication _ldapAuthentication;

        public AuthService(ILdapAuthentication ldapAuthentication)
        {
            _ldapAuthentication = ldapAuthentication ?? throw new ArgumentNullException(nameof(ldapAuthentication));
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (!request.UserName.EndsWith(AppConstant.EurolandEmail, StringComparison.CurrentCultureIgnoreCase))
                throw new AppException("Invalid credentials");

            var userName = request.UserName.Split("@")[0];


            var checkAuth = _ldapAuthentication.Authenticate(request.UserName, request.Password);

            if (!checkAuth)
            {
                throw new AppException("Invalid credentials");
            }

            var result = new LoginResponse(userName, "");

            return result;

        }
    }
}
