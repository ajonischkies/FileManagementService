using MongoDB.Driver;
using RedSky.FileManagement.Contracts.Entities;

namespace RedSky.FileManagement.Contracts.Context
{
    public interface IFileManagementMongoDbContext
    {
        IMongoCollection<FileData> Files { get; }
    }
}
