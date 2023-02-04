using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public sealed class RestaurantEntityDto
{
    public List<MenuDto> Menus { get; set; }
    public List<ReviewDto> Reviews { get; set; }
    public List<BookingDto> Bookings { get; set; }
    public List<TableDto> Tables { get; set; }
    public LocationDto Location { get; set; }
}
