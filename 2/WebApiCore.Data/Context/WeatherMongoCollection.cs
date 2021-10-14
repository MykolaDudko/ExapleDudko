using System;
using System.Collections.Generic;
using MongoDB.Driver;
using WebApiCore.Data.Models;
using System.Linq.Expressions;

namespace WebApiCore.Data.Context 
{
    public class WeatherMongoCollection
    {
        private readonly IMongoCollection<CurrentWeather> _weathers;

        public WeatherMongoCollection(IMongoCollection<CurrentWeather> _weathers)
        {
            this._weathers = _weathers;
        }

        public List<CurrentWeather> ToList() =>
            _weathers.Find(CurrentWeather => true).ToList();

        public CurrentWeather Get(string id) =>
            _weathers.Find<CurrentWeather>(CurrentWeather => CurrentWeather.Id == id).FirstOrDefault();
        public CurrentWeather FirstOrDefault(Expression<Func<CurrentWeather, bool>> predicator) =>
            _weathers.Find<CurrentWeather>(predicator).FirstOrDefault();

        public CurrentWeather Add(CurrentWeather CurrentWeather)
        {
            _weathers.InsertOne(CurrentWeather);
            return CurrentWeather;
        }

        public string Update(CurrentWeather WeatherIn) =>
            _weathers.ReplaceOne(CurrentWeather => CurrentWeather.Id == WeatherIn.Id, WeatherIn).UpsertedId.ToString();

        public void Remove(CurrentWeather WeatherIn) =>
            _weathers.DeleteOne(CurrentWeather => CurrentWeather.Id == WeatherIn.Id);

        public void Remove(string id) => 
            _weathers.DeleteOne(CurrentWeather => CurrentWeather.Id == id);
    }
}