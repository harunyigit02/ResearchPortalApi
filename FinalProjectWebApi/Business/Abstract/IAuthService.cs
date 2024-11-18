namespace FinalProjectWebApi.Business.Abstract
{
    public interface IAuthService
    {
        Task<string> Register(string username, string password);
        Task<string> Login(string username, string password);
    }
}
