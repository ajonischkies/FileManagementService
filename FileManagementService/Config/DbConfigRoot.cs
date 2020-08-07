namespace RedSky.FileManagement.Api.Config
{
    public class DbConfigRoot
    {
        private MongoDbConfig _mongoDb;

        public MongoDbConfig MongoDb
        {
            get
            {
                if(_mongoDb == null)
                {
                    _mongoDb = new MongoDbConfig();
                }

                return _mongoDb;
            }
        }
    }
}
