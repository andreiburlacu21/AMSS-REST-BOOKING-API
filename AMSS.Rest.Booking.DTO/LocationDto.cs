using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public sealed class LocationDto
{
    public int LocationId { get; set; }
    public string? X { get; set; }
    public string? Y { get; set; }
    public string? Adress { get; set; }
}
