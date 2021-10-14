using System.Threading.Tasks;
using WebApiCore.Data.Models;

namespace WebApiCore.Api.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetWeatherAsync(float lat, float lon);    
    }
}