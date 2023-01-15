using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Model.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMSS.Rest.Booking.Controllers;

[Route("AMSS/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IServiceRestaurants _restaurantService;
    public RestaurantController(IServiceRestaurants restaurantService)
    {
        _restaurantService = restaurantService;
    }

    #region Crud Operation

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _restaurantService.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Insert([FromBody] RestaurantDto restaurant)
    {
        try
        {
            return Ok(await _restaurantService.InsertAsync(restaurant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] RestaurantDto restaurant)
    {
        try
        {
            //await Check(restaurant.RestaurantId);

            return Ok(await _restaurantService.UpdateAsync(restaurant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            //await Check(id);

            return Ok(await _restaurantService.DeleteAsync(new RestaurantDto { RestaurantId = id }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion

    //#region Other Operation

    //[HttpGet("Entity/{id}")]
    //public async Task<IActionResult> GetRestaurantEntityById(int id)
    //{
    //    try
    //    {
    //        return Ok(await _restaurantService.GetRestaurantEnitityAsync(id));
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //[HttpGet("MyEntities")]
    //public async Task<IActionResult> GetMyRestaurantEntities()
    //{
    //    try
    //    {
    //        var id = int.Parse(User.FindFirst("Identifier")?.Value);

    //        return Ok(await _restaurantService.GetMyRestaurantEntitiesAsync(id));
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //#endregion

    #region Private methods

    #endregion
}
