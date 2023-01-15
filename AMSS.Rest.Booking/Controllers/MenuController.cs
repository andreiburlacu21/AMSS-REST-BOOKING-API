using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Model.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMSS.Rest.Booking.Controllers;

[Route("AMSS/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IServiceMenus _menuService;
    public MenuController(IServiceMenus menuService)
    {
        _menuService = menuService;
    }

    #region Crud Operation

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _menuService.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Insert([FromBody] MenuDto menu)
    {
        try
        {
            return Ok(await _menuService.InsertAsync(menu));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] MenuDto menu)
    {
        try
        {
            //await Check(menu.MenuId);

            return Ok(await _menuService.UpdateAsync(menu));
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

            return Ok(await _menuService.DeleteAsync(new MenuDto { MenuId = id }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion

    //#region Other Operation

    //[HttpGet("Entity/{id}")]
    //public async Task<IActionResult> GetMenuEntityById(int id)
    //{
    //    try
    //    {
    //        return Ok(await _menuService.GetMenuEnitityAsync(id));
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //[HttpGet("MyEntities")]
    //public async Task<IActionResult> GetMyMenuEntities()
    //{
    //    try
    //    {
    //        var id = int.Parse(User.FindFirst("Identifier")?.Value);

    //        return Ok(await _menuService.GetMyMenuEntitiesAsync(id));
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

