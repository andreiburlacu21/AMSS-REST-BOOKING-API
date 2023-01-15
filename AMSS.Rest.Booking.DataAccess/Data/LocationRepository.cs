using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class LocationRepository : Repository<Domains.Location>, ILocationRepository
{
    public LocationRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}