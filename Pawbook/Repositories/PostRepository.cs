using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;

namespace Pawbook.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(PawbookContext pawbookContext) : base(pawbookContext)
        {
        }
    }
}
