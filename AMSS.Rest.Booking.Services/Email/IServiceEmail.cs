using AMSS.Rest.Booking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Services.Email
{
    public interface IServiceEmail
    {
        Task<dynamic> ConfirmBooking(int userId, string key, BookingDto bookingDto);
        Task SendRentFinishedEmailAsync(int userId);
        Task SendRentMadeEmailAsync(int userId, BookingDto bookingDto);
    }
}
