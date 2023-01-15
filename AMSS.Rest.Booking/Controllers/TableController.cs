using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Model.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMSS.Rest.Booking.Controllers;

[Route("AMSS/[controller]")]
[ApiController]
public class TableController : ControllerBase
{
    private readonly IServiceTables _tableService;
    public TableController(IServiceTables tableService)
    {
        _tableService = tableService;
    }

    #region Crud Operation

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _tableService.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Insert([FromBody] TableDto table)
    {
        try
        {
            return Ok(await _tableService.InsertAsync(table));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] TableDto table)
    {
        try
        {
            //await Check(table.TableId);

            return Ok(await _tableService.UpdateAsync(table));
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

            return Ok(await _tableService.DeleteAsync(new TableDto { TableId = id }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion

    //#region Other Operation

    //[HttpGet("Entity/{id}")]
    //public async Task<IActionResult> GetTableEntityById(int id)
    //{
    //    try
    //    {
    //        return Ok(await _tableService.GetTableEnitityAsync(id));
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //[HttpGet("MyEntities")]
    //public async Task<IActionResult> GetMyTableEntities()
    //{
    //    try
    //    {
    //        var id = int.Parse(User.FindFirst("Identifier")?.Value);

    //        return Ok(await _tableService.GetMyTableEntitiesAsync(id));
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

