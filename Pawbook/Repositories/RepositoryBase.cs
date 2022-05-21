using Microsoft.EntityFrameworkCore;
using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using System.Linq.Expressions;

namespace Pawbook.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PawbookContext pawbookContext { get; set; }

        public RepositoryBase(PawbookContext pawbookContext)
        {
            this.pawbookContext = pawbookContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.pawbookContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.pawbookContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.pawbookContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.pawbookContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.pawbookContext.Set<T>().Remove(entity);
        }
    }
}
