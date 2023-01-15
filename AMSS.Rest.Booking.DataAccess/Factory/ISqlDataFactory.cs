using AMSS.Rest.Booking.DataAccess.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Factory;

public interface ISqlDataFactory
{
    IAccountsRepository AccountsRepository { get; }
    IBookingRepository BookingRepository { get; }   
    ILocationRepository LocationRepository { get; }
    IMenuRepository MenuRepository { get; }
    IRestaurantRepository RestaurantRepository { get; }
    IReviewRepository ReviewRepository { get; }
    ITableRepository TableRepository { get; }   

}
