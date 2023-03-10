using AMSS.Rest.Booking.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMSS.Rest.Booking.Controllers;

[Route("AMSS/[controller]")]
[ApiController]
[Authorize]
public class PicturesController : ControllerBase
{
    private readonly IWebHostEnvironment? webHostEnvironment;
    public PicturesController(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }


    [HttpPost("UpdateImage")]
    public async Task<IActionResult> UpdateImages([FromForm] UploadPhotoDto file)
    {
        try
        {
            var path = webHostEnvironment.ContentRootPath + $"/Images/{file.Type}/{file.Id}/";

            if (file.File.Length > 0)
            {
                await SavePicture(path, file.File);
            }
            return Ok(true);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetImages/{type}/{id}")]
    public IActionResult GetFilesLocations(string type, int id)
    {
        try
        {
            return Ok(this.GetPicturesListAsync(type, id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteImage/{type}/{id}/{fileName}")]
    public IActionResult DeleteFile(string type, int id, string fileName)
    {
        try
        {
            var path = webHostEnvironment.ContentRootPath + $"/Images/{type}/{id}/{fileName}";

            System.IO.File.Delete(path);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task SavePicture(string path, IFormFile file)
    {
        Directory.CreateDirectory(path);

        using var fileStream = System.IO.File.Create(path + file.FileName);

        await file.CopyToAsync(fileStream);

        await fileStream.FlushAsync();
    }

    private List<string> GetPicturesListAsync(string type, int id)
    {
        var location = $"/Images/{type}/{id}/";

        var path = webHostEnvironment.ContentRootPath + location;

        var host = "https:" + Request.Host.Value + location;

        return Directory.GetFiles(path)
                        .Select(file => host + Path.GetFileName(file))
                        .ToList();

    }
}

