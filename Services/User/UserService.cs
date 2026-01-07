using Models.DBModel;
using Models.Request;
using Providers.User;

namespace Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserProvider _userProvider;
        public UserService(IUserProvider userProvider) 
        {
            _userProvider = userProvider;
        }

        public async Task<int> CreateUser(RegisterRequest user)
        {
            user.Password = BC.EnhancedHashPassword(user.Password, 13);
            int userId = await _userProvider.CreateUser(user);
            return userId;
        }

        public async Task<AppUser> GetUserById(int Id)
        {
            return await _userProvider.GetUserById(Id);
        }

        public async Task UpdateUser(int id, UpdateUser request)
        {
            var existingUser = await GetUserById(id);

            if (existingUser == null)
                throw new ArgumentException("User not found");

            AppUser appUser = new AppUser
            {
                Id = existingUser.Id,
                Name = request.Name ?? existingUser.Name,
                Email = request.Email ?? existingUser.Email,
                Phone = request.Phone ?? existingUser.Phone
            };

            await _userProvider.UpdateUser(appUser);
        }
    }
}
