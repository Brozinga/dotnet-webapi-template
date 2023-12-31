using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Template.WebApi.Controllers;
using Template.WebApi.Model;
using Template.WebApi.Services;

namespace Template.WebApi.Controllers.V2;

[ApiVersion("2.0")]
public class WeatherForecastController : ApiControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger)
    {
        _weatherForecastService = weatherForecastService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        WeatherForecast weatherForecast;
        try
        {
            weatherForecast = _weatherForecastService.Get();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Ocorreu um erro");
            return BadRequest(exception.Message);
        }

        return Ok(weatherForecast);
    }
}
