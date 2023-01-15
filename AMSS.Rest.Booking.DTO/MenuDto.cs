using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public sealed class MenuDto
{
    public int MenuId { get; set; }
    public int RestaurantId { get; set; }
    public string? Content { get; set; }
}
