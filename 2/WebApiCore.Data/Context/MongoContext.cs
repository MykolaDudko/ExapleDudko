using System.Collections.Generic;
using MongoDB.Driver;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Context 
{
    public class MongoContext
    {
        private IMongoDatabase database;

        public CustomersMongoCollection Customers { get; private set; }
        public WeatherMongoCollection Weathers { get; private set; }

        public MongoContext(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            Customers = new CustomersMongoCollection(database.GetCollection<Customer>(settings.CustomersCollectionName));
            Weathers = new WeatherMongoCollection(database.GetCollection<CurrentWeather>(settings.WeathersCollectionName));
        }
    }
}