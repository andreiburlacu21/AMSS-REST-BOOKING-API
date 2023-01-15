using AMSS.Rest.Booking.DataAccess.Factory;
using AMSS.Rest.Booking.Service.Authentification;
using AMSS.Rest.Booking.Service.Model;
using AMSS.Rest.Booking.Service.Model.Contracts;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AMSS.Rest.Booking.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration config)
    {


        services.AddTransient<IServiceAccounts, ServiceAccounts>();

        services.AddTransient<IServiceBookings, ServiceBookings>();

        services.AddTransient<IServiceLocations, ServiceLocations>();

        services.AddTransient<IServiceMenus, ServiceMenus>();

        services.AddTransient<IServiceRestaurants, ServiceRestaurants>();

        services.AddTransient<IServiceReviews, ServiceReviews>();

        services.AddTransient<IServiceTables, ServiceTables>();

        services.AddTransient<IServiceAuthentification>
            (
               provider => new ServiceAuthentification(
                   provider.GetService<ISqlDataFactory>(),
                   config.GetConnectionString("MySecretKey"),
                   provider.GetService<IMapper>())
            );

        return services;
    }
}
