using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class TableRepository : Repository<Domains.Table>, ITableRepository
{
    public TableRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}