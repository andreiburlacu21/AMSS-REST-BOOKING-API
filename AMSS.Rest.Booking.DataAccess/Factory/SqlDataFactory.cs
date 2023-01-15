using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Data;
using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Factory
{
    public class SqlDataFactory : ISqlDataFactory
    {
        private readonly ISqlDataAccess sqlDataAccess;
        public IAccountsRepository AccountsRepository => new AccountsRepository(sqlDataAccess);

        public IBookingRepository BookingRepository => new BookingRepository(sqlDataAccess);

        public ILocationRepository LocationRepository => new LocationRepository(sqlDataAccess);

        public IMenuRepository MenuRepository => new MenuRepository(sqlDataAccess);

        public IRestaurantRepository RestaurantRepository => new RestaurantRepository(sqlDataAccess);

        public IReviewRepository ReviewRepository => new ReviewRepository(sqlDataAccess);

        public ITableRepository TableRepository => new TableRepository(sqlDataAccess);
        public SqlDataFactory(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }
    }
}
