using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForcastService _service;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForcastService service)
    {
        _logger = logger;
        _service = service;
    }
    
    [HttpPost("generate")]
    public ActionResult<IWeatherForcastService> Generate([FromQuery]int count, [FromBody]TemperatureRequest request)
    {
        if(count < 0 || request.Min > request.Max)
            return BadRequest("Invalid parameters");
        
        var result = _service.Get2(count, request.Min, request.Max);
        return Ok(result);
    }
}