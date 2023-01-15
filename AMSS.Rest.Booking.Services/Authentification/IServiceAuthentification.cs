using AMSS.Rest.Booking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Service.Authentification;

public interface IServiceAuthentification
{
Task<dynamic> GenerateTokenAsync(AuthDto authData);
Task<bool> IsValidUserNameAndPassowrdAsync(AuthDto authData);
Task<AccountDto> RegisterAsync(AccountDto user);
}