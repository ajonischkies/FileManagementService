using RedSky.FileManagement.Contracts.Repositories;
using RedSky.FileManagement.Contracts.Services;

namespace RedSky.FileManagement.Business.Services
{
    public class ServiceWrapper : IServiceWrapper
    {
        private IRepositoryWrapper _repository;

        public ServiceWrapper(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        private IFileService _files;

        public IFileService Files
        {
            get
            {
                if (_files == null) _files = new FileService(_repository);
                return _files;
            }
        }
    }
}
