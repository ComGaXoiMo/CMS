using CMS_1.Models;

namespace CMS_1.System
{
    public interface IUserService
    {
        Task<LoginResponse> Authenticate(LoginRequest model);
        Task<ForgotPasswordResponse>  ForgotPassword(FogotPasswordRequest model);
        Task<RecoverPasswordResponse>  RecoverPassword(RecoverPasswordRequest model);
        void Save(User user);

    }
}
