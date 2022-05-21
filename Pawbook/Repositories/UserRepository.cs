using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;

namespace Pawbook.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PawbookContext pawbookContext) : base(pawbookContext)
        {
        }
    }
}
