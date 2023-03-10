using AMSS.Rest.Booking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Service.Model.Contracts;

public interface IServiceRestaurants : IService<RestaurantDto>
{
    Task<RestaurantEntityDto> GetInfoRestaurant(int resturantId);
}
