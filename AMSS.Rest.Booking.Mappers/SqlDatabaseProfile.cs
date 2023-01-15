using AMSS.Rest.Booking.DataAccess.Data.Domains;
using AMSS.Rest.Booking.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Mapper;

public sealed class SqlDatabaseProfile : Profile
{
    public SqlDatabaseProfile()
    {
        CreateMap<AccountDto, Account>().ReverseMap();
        CreateMap<BookingDto, DataAccess.Data.Domains.Bookings>().ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<MenuDto, Menu>().ReverseMap();
        CreateMap<RestaurantDto, Restaurant>().ReverseMap();
        CreateMap<TableDto, Table>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();

    }
}
