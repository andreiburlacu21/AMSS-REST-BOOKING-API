using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Domains;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class AccountsRepository : Repository<Account>, IAccountsRepository
{
    public AccountsRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}