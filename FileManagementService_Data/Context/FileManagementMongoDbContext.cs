using MongoDB.Driver;
using RedSky.FileManagement.Contracts.Config;
using RedSky.FileManagement.Contracts.Context;
using RedSky.FileManagement.Contracts.Entities;

namespace RedSky.FileManagement.Data.Context
{
    public class FileManagementMongoDbContext : IFileManagementMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public FileManagementMongoDbContext(IMongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _database = client.GetDatabase(config.Database);
        }

        public IMongoCollection<FileData> Files => _database.GetCollection<FileData>("Files");
    }
}
