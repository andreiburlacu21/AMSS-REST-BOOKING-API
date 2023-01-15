using AMSS.Rest.Booking.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Validator.Model.Validation;

public sealed class LocationValidation : AbstractValidator<LocationDto>
{
    public LocationValidation()
    {
        RuleFor(loc => loc.Address)
            .NotEmpty();
    }
}
