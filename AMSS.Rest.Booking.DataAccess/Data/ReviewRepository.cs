using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class ReviewRepository : Repository<Domains.Review>, IReviewRepository
{
    public ReviewRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}