using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public class UploadPhotoDto
{
    public IFormFile? File { get; set; }
    public int Id { get; set; }
    public string? Type { get; set; }
}
