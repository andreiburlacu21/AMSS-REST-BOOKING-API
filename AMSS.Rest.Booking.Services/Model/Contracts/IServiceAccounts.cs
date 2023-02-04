using AMSS.Rest.Booking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Service.Model.Contracts;

public interface IServiceAccounts : IService<AccountDto>
{
    Task<AccountDto> SearchByUserNameAsync(string userName);
    Task<AccountDto> SearchByEmailAsync(string email);
    Task<AccountEntityDto> GetAccountInfoAsync(int accountId);
}
