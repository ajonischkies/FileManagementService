using RedSky.FileManagement.Contracts.Entities;
using RedSky.FileManagement.Contracts.Repositories;
using RedSky.FileManagement.Tests.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Tests.Mocks
{
    public class MockFileRepository : IFileRepository
    {
        private readonly List<FileData> _files;

        public MockFileRepository(int count)
        {
            _files = new List<FileData>();

            for (int i = 1; i <= count; i++)
            {
                _files.Add(new StubFileData().GetNew(i));
            }
        }

        public async Task<FileData> Add(FileData entity)
        {
            entity.Id = _files.Count > 0 ? _files.Max(file => file.Id) + 1 : 1;
            _files.Add(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            FileData file = _files.FirstOrDefault(file => file.Id == id);
            if (file != null) _files.Remove(file);
            return;
        }

        public async Task DeleteRange(IEnumerable<int> ids)
        {
            List<FileData> filesToRemove = _files.FindAll(file => ids.Contains(file.Id));
            foreach(FileData file in filesToRemove)
            {
                _files.Remove(file);
            }
            return;
        }

        public IQueryable<FileData> Get()
        {
            return _files.AsQueryable();
        }

        public IQueryable<FileData> Search(Expression<Func<FileData, bool>> expression)
        {
            return _files.AsQueryable().Where(expression);
        }
    }
}
