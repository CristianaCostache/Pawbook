using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUserByName(string searchString);
        void Update(User user);
        bool UserExist(User user);
        void Register(User user);
        bool PasswordMatch(User user);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
    }
}
