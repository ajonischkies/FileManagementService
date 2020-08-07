using MongoDB.Driver;
using RedSky.FileManagement.Contracts.Context;
using RedSky.FileManagement.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    {
        protected IFileManagementMongoDbContext _context;

        public RepositoryBase(IFileManagementMongoDbContext context)
        {
            _context = context;
        }

        public abstract Task Delete(int id);

        public abstract Task DeleteRange(IEnumerable<int> ids);

        public abstract IQueryable<T> Get();

        public abstract IQueryable<T> Search(Expression<Func<T, bool>> expression);

        public abstract Task<T> Add(T entity);
    }
}
