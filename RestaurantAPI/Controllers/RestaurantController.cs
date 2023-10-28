using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services.IServices;

namespace RestaurantAPI.Controllers;

[ApiController]
[Route("api/restaurant")]
[Authorize]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _restaurantService.Delete(id);
        
        return NoContent();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        var id =_restaurantService.Create(dto);
        
        return Created($"/api/restaurant/{id}", null);
    }
    
    [HttpGet]
    [Authorize(Policy = "AtLeast20")]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurantsDtos = _restaurantService.GetAll();
        
        return Ok(restaurantsDtos);
    }
    
    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> Get([FromRoute] int id)
    {
        var restaurantDto = _restaurantService.GetById(id);
        
        return Ok(restaurantDto);
    }
    
    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
    {
        _restaurantService.Update(id, dto);
        
        return Ok();
    }
}