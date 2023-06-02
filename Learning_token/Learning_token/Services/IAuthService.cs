
using Learning_token.Models.Custom;

namespace Learning_token.Services
{
    public interface IAuthService
    {



        Task<AuthResponse> GetToken(AuthRequest authorization);
    }
}
