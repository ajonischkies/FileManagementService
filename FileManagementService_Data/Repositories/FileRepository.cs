using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RedSky.FileManagement.Contracts.Context;
using RedSky.FileManagement.Contracts.Entities;
using RedSky.FileManagement.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Data.Repositories
{
    public class FileRepository : RepositoryBase<FileData>, IFileRepository
    {
        public FileRepository(IFileManagementMongoDbContext context) : base(context)
        {
        }

        public override async Task<FileData> Add(FileData entity)
        {
            try
            {
                entity.Id = await _context.Files.AsQueryable().MaxAsync(file => file.Id) + 1;
            }
            catch
            {
                entity.Id = 1;
            }

            await _context.Files.InsertOneAsync(entity);

            FileData updated = (await _context.Files.FindAsync(file => file.Id == entity.Id)).FirstOrDefault();
            return updated;
        }

        public override async Task Delete(int id)
        {
            await _context.Files.FindOneAndDeleteAsync(file => file.Id == id);
        }

        public override async Task DeleteRange(IEnumerable<int> ids)
        {
            await _context.Files.DeleteManyAsync(file => ids.Contains(file.Id));
        }

        public override IQueryable<FileData> Get()
        {
            return _context.Files.AsQueryable();
        }

        public override IQueryable<FileData> Search(Expression<Func<FileData, bool>> expression)
        {
            return _context.Files.AsQueryable().Where(expression);
        }
    }
}
