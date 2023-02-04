using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public class AccountEntityDto : AccountDto
{
    public List<ReviewDto> Reviews { get; set; }
    public List<BookingDto> Bookings { get;set; }
}
