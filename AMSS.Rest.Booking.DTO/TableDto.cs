using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public sealed class TableDto
{
    public int TableId { get; set; }
    public int RestauranId { get; set; }
    public int NumberOfSeats { get; set; }
    public bool Outdoor { get; set; }

}
