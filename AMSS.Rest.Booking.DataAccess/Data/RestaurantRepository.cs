using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class RestaurantRepository : Repository<Domains.Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}