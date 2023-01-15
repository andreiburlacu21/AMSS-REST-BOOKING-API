using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public sealed class ReviewDto
{
    public int ReviewId { get; set; }
    public int BookingId { get; set; }
    public int Grade { get; set; }
    public string? Description { get; set; }
}
