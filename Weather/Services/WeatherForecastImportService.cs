using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Weather.DbContexts;
using Weather.Models;

namespace Weather.Services
{
    public class WeatherForecastImportService : ImportService<WeatherForecast>
    {
        private readonly WeatherForecastDbContext _db;

        private List<WeatherForecastRecord> weatherForecastRecords;

        public WeatherForecastImportService(WeatherForecastDbContext db)
        {
            _db = db;
            weatherForecastRecords = new List<WeatherForecastRecord>();
        }

        protected override bool SkipFirstLine => true;
        protected override char ColumnDelimiter => ';';

        protected override async Task ReadAsync(StreamReader reader)
        {
            if (SkipFirstLine)
                await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var row = (await reader.ReadLineAsync()).Split(ColumnDelimiter);

                DateTime date;
                double temperature;

                if (!DateTime.TryParse(row[0], out date))
                    throw new Exception("Invalid date");

                if (!double.TryParse(row[1], out temperature))
                    throw new Exception("Invalid temperature");

                string summary = row[2];

                weatherForecastRecords.Add(new WeatherForecastRecord(date, temperature, summary));
            }

            _db.WeatherForecasts.AddRange(weatherForecastRecords.Select(w => new WeatherForecast
            {
                Date = w.Date,
                Temperature = w.Temperature,
                Summary = w.Summary
            }));

            await _db.SaveChangesAsync();
        }
    }

    public class WeatherForecastRecord
    {
        public DateTime Date { get; set; }

        public double Temperature { get; set; }

        public string Summary { get; set; }

        public WeatherForecastRecord(DateTime date, double temperature, string summary)
        {
            Date = date;
            Temperature = temperature;
            Summary = summary;
        }
    }
}
