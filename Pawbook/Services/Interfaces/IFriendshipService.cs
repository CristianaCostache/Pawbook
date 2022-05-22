using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface IFriendshipService
    {
        void AddFriendship(int userId, int loggedInUserId);
        List<Friendship> GetFriendshipByUserId(int userId);
        bool IsFriendWith(int userId);
    }
}
