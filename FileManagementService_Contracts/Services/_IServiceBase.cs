using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Contracts.Services
{
    public interface IServiceBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> Search(Expression<Func<T, bool>> expression);
        T Download(int id);
        Task<T> Add(T entity);
        Task Delete(int id);
        Task DeleteRange(IEnumerable<int> ids);
    }
}
