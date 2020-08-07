using RedSky.FileManagement.Contracts.Entities;
using RedSky.FileManagement.Contracts.Repositories;
using RedSky.FileManagement.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Business.Services
{
    public class FileService : ServiceBase<FileData>, IFileService
    {
        public FileService(IRepositoryWrapper repository) : base(repository)
        {
        }

        public override async Task<FileData> Add(FileData entity)
        {
            //Add any business logic and logging here

            return await _repository.Files.Add(entity);
        }

        public override async Task Delete(int id)
        {
            //Add any business logic and logging here

            await _repository.Files.Delete(id);
        }

        public override async Task DeleteRange(IEnumerable<int> ids)
        {
            //Add any business logic and logging here

            await _repository.Files.DeleteRange(ids);
        }

        public override IQueryable<FileData> GetAll()
        {
            //Add any business logic and logging here

            return _repository.Files.Get();
        }

        public override FileData Download(int id)
        {
            //Add any business logic and logging here

            FileData file = _repository.Files.Search(file => file.Id == id).FirstOrDefault();
            return file;
        }

        public override IQueryable<FileData> Search(Expression<Func<FileData, bool>> expression)
        {
            //Add any business logic and logging here

            return _repository.Files.Search(expression);
        }
    }
}
