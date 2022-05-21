using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUserByName(string searchString);
        void Update(User user);
    }
}
