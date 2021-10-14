using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Api.Services;
using WebApiCore.Data.Models;
using WebApiCore.Data.Repository;

namespace WebApiCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IRepository<CurrentWeather> contextWeather;
        private readonly IWeatherService weatherService;

        public WeatherController(IRepository<CurrentWeather> contextWeather, IWeatherService weatherService)
        {
            this.contextWeather = contextWeather;
            this.weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<CurrentWeather> Get()
        {
            return contextWeather.All;
        }

        [HttpGet("currentWeather")]
        [ProducesResponseType(typeof(IEnumerable<CurrentWeather>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CurrentWeather> Get([FromServices] IWeatherService ws, float lat, float lon)
        {
            var res = await ws.GetWeatherAsync(lat, lon);
            contextWeather.Add(res);

            
            return res;
        }

        [HttpPost]
        public void Post([FromQuery] CurrentWeather value)
        {
            contextWeather.Update(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CurrentWeather value)
        {
            contextWeather.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = contextWeather.FindById(id);
            contextWeather.Delete(entity);
        }

        private void HiddenForRequest() { 

        }
        
        [NonAction]
        public void HiddenForRequestToo() { 
            
        }
    }
}