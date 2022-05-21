using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;

namespace Pawbook.Repositories
{
    public class PawRepository : RepositoryBase<Paw>, IPawRepository
    {
        public PawRepository(PawbookContext pawbookContext) : base(pawbookContext)
        {
        }
    }
}
