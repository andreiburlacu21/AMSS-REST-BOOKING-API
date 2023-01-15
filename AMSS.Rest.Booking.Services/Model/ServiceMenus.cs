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

public class ServiceMenus : IServiceMenus
{
    #region Fields
    private readonly ISqlDataFactory _repositories;
    private readonly IValidator<MenuDto> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ServiceMenus(ISqlDataFactory repositories, IValidator<MenuDto> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods     
    public async Task<bool> DeleteAsync(MenuDto value)
    {
        var menu = _mapper.Map<Menu>(value);

        return await _repositories.MenuRepository.DeleteAsync(menu);
    }

    public async Task<List<MenuDto>> GetAllAsync()
    {
        var menu = await _repositories.MenuRepository.GetAllAsync();

        return _mapper.Map<List<MenuDto>>(menu);
    }

    public async Task<MenuDto> InsertAsync(MenuDto value)
    {
        var acc = await _repositories.MenuRepository.FirstOrDefaultAsync(x => x.Content == value.Content &&
                                                                              x.RestaurantId == value.RestaurantId);

        if (acc is not null)
            throw new ValidationException("Menu already exists");

        await Validate.FluentValidate(_validator, value);

        var menu = _mapper.Map<Menu>(value);

        var menuDto = await _repositories.MenuRepository.InsertAsync(menu);

        return _mapper.Map<MenuDto>(menuDto);
    }

    public async Task<MenuDto> SearchByIdAsync(int id)
    {
        var menuDto = await _repositories.MenuRepository.SearchByIdAsync(id);

        return _mapper.Map<MenuDto>(menuDto);
    }

    public async Task<MenuDto> UpdateAsync(MenuDto value)
    {
        var menuSearch = await _repositories.MenuRepository.SearchByIdAsync(value.MenuId);

        if (menuSearch is null)
            throw new ValidationException("Menu does not exists");

        await Validate.FluentValidate(_validator, value);

        var menu = await _repositories.MenuRepository.UpdateAsync(_mapper.Map<Menu>(value));

        return _mapper.Map<MenuDto>(menu);
    }
    #endregion
}

