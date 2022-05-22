using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;

namespace Pawbook.Repositories
{
    public class FriendshipRepository : RepositoryBase<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(PawbookContext pawbookContext) : base(pawbookContext)
        {
        }
    }
}
