using AMSS.Rest.Booking.DataAccess.Connections;
using AMSS.Rest.Booking.DataAccess.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Configuration;
public static class RepositoryConfiguration
{
    public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<ISqlDataAccess>(new SqlDataAccess(config.GetConnectionString("DataBase") ?? throw new ArgumentNullException("config")));

        services.AddSingleton<ISqlDataFactory, SqlDataFactory>();

        return services;
    }
}
