using Models.Login;
using RestSharp;

namespace Pokedex_MAUI.Services;

public class LoginService: ILoginService
{
    public async Task<Users> Login(string email, string password)
    {
        var client = new RestClient("http://localhost:5190/api/User/");
        var request = new RestRequest(email + "/" + password, Method.Get);
        var response = await client.ExecuteAsync<Users>(request);
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        return null;
    }
    
    public async Task<Users> Register(string fullName, string email, string password)
    {
        var client = new RestClient("http://localhost:5190/api/User/");
        var request = new RestRequest("",Method.Post);
        request.AddJsonBody(new Users {FullName = fullName, Email = email, Password = password });
        var response = await client.ExecuteAsync<Users>(request);
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        return null;
    }
}