using RedSky.FileManagement.Contracts.Context;
using RedSky.FileManagement.Contracts.Repositories;

namespace RedSky.FileManagement.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IFileManagementMongoDbContext _context;

        public RepositoryWrapper(IFileManagementMongoDbContext context)
        {
            _context = context;
        }

        private IFileRepository _files;

        public IFileRepository Files
        {
            get
            {
                if (_files == null) _files = new FileRepository(_context);
                return _files;
            }
        }
    }
}
