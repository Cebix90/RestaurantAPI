using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services.IServices;

namespace RestaurantAPI.Controllers;

[ApiController]
[Route("api/restaurant")]
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
        var isDeleted = _restaurantService.Delete(id);

        if (isDeleted is false) return NotFound();
        
        return NoContent();
    }
    
    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        if(ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }
        
        var id =_restaurantService.Create(dto);
        
        return Created($"/api/restaurant/{id}", null);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurantsDtos = _restaurantService.GetAll();
        
        return Ok(restaurantsDtos);
    }
    
    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> Get([FromRoute] int id)
    {
        var restaurantDto = _restaurantService.GetById(id);
        
        if (restaurantDto is null)
        {
            return NotFound();
        }
        
        return Ok(restaurantDto);
    }
    
    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var isUpdated = _restaurantService.Update(id, dto);
        
        if (isUpdated is false)
        {
            return NotFound();
        }
        
        return Ok();
    }
}