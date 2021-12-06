using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.DataAccess;
using Weather.Dto;
using Weather.Models;
using Weather.Services;

namespace Weather.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        //private readonly IQueryProcessor QueryProcessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService /*, IQueryProcessor queryProcessor*/)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            //QueryProcessor = queryProcessor;
        }

        /// <summary>
        /// Get list of weather forecasts
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get(
            /*[FromQuery] FetchWeatherForecastQuery query*/)
        {
            _logger.LogInformation("Fetching weather forecasts");

            var entities = await _weatherForecastService.GetWeatherForecastAsync(); //await QueryProcessor.ProcessAsync(query);

            _logger.LogInformation("Returning weather forecasts");

            // NOTE: Missing mapping

            return Ok(entities);
        }

        /// <summary>
        /// Import wheater forecast csv file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromForm]CSVFileDto file,
            [FromServices]WeatherForecastImportService service)
        {
            _logger.LogInformation("Importing weather forecast csv file");

            var stream = file.File.OpenReadStream();

            try
            {
                await service.ImportAsync(stream);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _logger.LogInformation("Imported weather forecast csv file");

            return Ok();
        }
    }
}
