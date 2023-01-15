using AMSS.Rest.Booking.DataAccess.Data.Domains;
using AMSS.Rest.Booking.DataAccess.Factory;
using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Model.Contracts;
using AMSS.Rest.Booking.Validator;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Service.Model;

public class ServiceLocations : IServiceLocations
{
    #region Fields
    private readonly ISqlDataFactory _repositories;
    private readonly IValidator<LocationDto> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ServiceLocations(ISqlDataFactory repositories, IValidator<LocationDto> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods     
    public async Task<bool> DeleteAsync(LocationDto value)
    {
        var locations = _mapper.Map<Location>(value);

        return await _repositories.LocationRepository.DeleteAsync(locations);
    }

    public async Task<List<LocationDto>> GetAllAsync()
    {
        var locations = await _repositories.LocationRepository.GetAllAsync();

        return _mapper.Map<List<LocationDto>>(locations);
    }

    public async Task<LocationDto> InsertAsync(LocationDto value)
    {
        var acc = await _repositories.LocationRepository.FirstOrDefaultAsync(x => x.X == value.X &&
                                                                                  x.Y == value.Y &&
                                                                                  x.Address == value.Address);

        if (acc is not null)
            throw new ValidationException("Location already exists");

        await Validate.FluentValidate(_validator, value);

        var location = _mapper.Map<Location>(value);

        var locationDto = await _repositories.LocationRepository.InsertAsync(location);

        return _mapper.Map<LocationDto>(locationDto);
    }

    public async Task<LocationDto> SearchByIdAsync(int id)
    {
        var locationsDto = await _repositories.LocationRepository.SearchByIdAsync(id);

        return _mapper.Map<LocationDto>(locationsDto);
    }

    public async Task<LocationDto> UpdateAsync(LocationDto value)
    {
        var locationsSearch = await _repositories.LocationRepository.SearchByIdAsync(value.LocationId);

        if (locationsSearch is null)
            throw new ValidationException("Location does not exists");

        await Validate.FluentValidate(_validator, value);

        var location = await _repositories.LocationRepository.UpdateAsync(_mapper.Map<Location>(value));

        return _mapper.Map<LocationDto>(location);
    }
    #endregion
}

