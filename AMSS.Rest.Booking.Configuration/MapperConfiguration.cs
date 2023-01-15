using AMSS.Rest.Booking;
using AMSS.Rest.Booking.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Configuration;

public static class MapperConfiguration
{
    public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(SqlDatabaseProfile));

        return services;
    }
}
