using Models.Request;

namespace Services.Account
{
    public interface IAccountService
    {
        Task<int> CreateAccount(string userId, CreateAccountRequest request);
    }
}
