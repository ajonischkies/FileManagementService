using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Contracts.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Get();
        IQueryable<T> Search(Expression<Func<T, bool>> expression);
        Task<T> Add(T entity);
        Task Delete(int id);
        Task DeleteRange(IEnumerable<int> ids);
    }
}
