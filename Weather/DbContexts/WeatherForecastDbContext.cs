using Microsoft.EntityFrameworkCore;
using Weather.Models;

namespace Weather.DbContexts
{
    public class WeatherForecastDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public string dbPath { get; set; }

        public WeatherForecastDbContext()
        {
            dbPath = "weatherForecast.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Data Source={dbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WeatherForecast>()
                .HasIndex(w => w.Id).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
