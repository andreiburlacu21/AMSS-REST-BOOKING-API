using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Validator.Model.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AMSS.Rest.Booking.Configuration;

public static class ValidatorConfiguration
{
    public static IServiceCollection AddValidatorConfiguration(this IServiceCollection services)
    {

        services.AddTransient<IValidator<AccountDto>, AccountValidation>();

        services.AddTransient<IValidator<BookingDto>, BookingValidation>();

        services.AddTransient<IValidator<LocationDto>, LocationValidation>();

        services.AddTransient<IValidator<ReviewDto>, ReviewValidation>();

        services.AddTransient<IValidator<MenuDto>, MenuValidation>();

        services.AddTransient<IValidator<RestaurantDto>, RestaurantValidation>();

        services.AddTransient<IValidator<TableDto>, TableValidation>();

        return services;
    }
}

