using System.Collections.Generic;
using System.Linq;
using WebApiCore.Data.Context;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Repository
{
    public class WeatherRepository : IRepository<CurrentWeather>
        {
            private readonly MongoContext context;

            public IEnumerable<CurrentWeather> All => context.Weathers.ToList();

            public WeatherRepository(MongoContext context)
            {
                this.context = context;
            }
            public CurrentWeather Add(CurrentWeather entity)
            {
                return context.Weathers.Add(entity);
            }

            public void Delete(CurrentWeather entity)
            {
                context.Weathers.Remove(entity);
            }

            public CurrentWeather FindById(string Id)
            {
                return context.Weathers.FirstOrDefault(e => e.Id == Id);
            }

            public string Update(CurrentWeather entity)
            {
                return context.Weathers.Update(entity);
            }
        }
}