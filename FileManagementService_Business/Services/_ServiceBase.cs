using RedSky.FileManagement.Contracts.Repositories;
using RedSky.FileManagement.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Business.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T>
    {
        protected IRepositoryWrapper _repository;

        public ServiceBase(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public abstract Task Delete(int id);

        public abstract Task DeleteRange(IEnumerable<int> ids);

        public abstract IQueryable<T> GetAll();

        public abstract T Download(int id);

        public abstract IQueryable<T> Search(Expression<Func<T, bool>> expression);

        public abstract Task<T> Add(T entity);
    }
}
