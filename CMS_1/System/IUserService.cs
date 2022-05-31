using CMS_1.Models;

namespace CMS_1.System
{
    public interface IUserService
    {
        LoginResponse Authenticate(LoginRequest model);
    }
}
