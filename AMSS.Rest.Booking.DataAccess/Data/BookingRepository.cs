using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using AMSS.Rest.Booking.DataAccess.Data.Reposiotry;

namespace AMSS.Rest.Booking.DataAccess.Data;

public sealed class BookingRepository : Repository<Domains.Bookings>, IBookingRepository
{
    public BookingRepository(ISqlDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}