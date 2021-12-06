using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Models
{
    public class WeatherForecast : IEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Temperature { get; set; }

        public string Summary { get; set; }
    }
}
