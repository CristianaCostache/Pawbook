using Pawbook.ViewModels;

namespace Pawbook.Services.Interfaces
{
    public interface IFeedItemService
    {
        List<FeedItem> GetAll(int? loggedInUserId);
        List<FeedItem> GetByUser(int userId, int isLoggedUser);
    }
}
