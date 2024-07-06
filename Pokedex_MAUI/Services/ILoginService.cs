using Models.Login;

namespace Pokedex_MAUI.Services;

public interface ILoginService
{
    Task<Users> Login(string email, string password);
    Task<Users> Register(string fullName, string email, string password);
}