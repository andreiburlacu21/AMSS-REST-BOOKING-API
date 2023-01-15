using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class MenuRepository : Repository<Domains.Menu>, IMenuRepository
{
    public MenuRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}