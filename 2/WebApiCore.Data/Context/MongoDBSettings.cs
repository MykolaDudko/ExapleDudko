namespace WebApiCore.Data.Context
{
        public class MongoDBSettings : IMongoDBSettings
    {
        public string CustomersCollectionName { get; set; }
        public string WeathersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string CustomersCollectionName { get; set; }
        string WeathersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}