
using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;

namespace Pawbook.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(PawbookContext pawbookContext) : base(pawbookContext)
        {
        }
    }
}
