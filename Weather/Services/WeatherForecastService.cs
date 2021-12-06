using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.DbContexts;
using Weather.Models;

namespace Weather.Services
{
    public class GetWeatherForecastService : IWeatherForecastService
    {
        private readonly WeatherForecastDbContext _db;

        public GetWeatherForecastService(WeatherForecastDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
        {
            return await _db.WeatherForecasts
                .OrderBy(w => w.Date)
                .ToListAsync();
        }
    }
}
