using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Services.Email
{
    public interface IServiceEmail
    {
        Task<dynamic> GetTokenForForgotPasswordAsync(string email, string key);
        Task SendRentFinishedEmailAsync(int userId);
        Task SendRentMadeEmailAsync(int userId);
        Task SendCreatedEmailAsync(string emailTo);
        Task SendForgotPasswordEmailAsync(string emailTo);
    }
}
