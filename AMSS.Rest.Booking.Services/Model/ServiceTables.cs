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

public class ServiceTables : IServiceTables
{
    #region Fields
    private readonly ISqlDataFactory _repositories;
    private readonly IValidator<TableDto> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ServiceTables(ISqlDataFactory repositories, IValidator<TableDto> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods     
    public async Task<bool> DeleteAsync(TableDto value)
    {
        var table = _mapper.Map<Table>(value);

        return await _repositories.TableRepository.DeleteAsync(table);
    }

    public async Task<List<TableDto>> GetAllAsync()
    {
        var table = await _repositories.TableRepository.GetAllAsync();

        return _mapper.Map<List<TableDto>>(table);
    }

    public async Task<TableDto> InsertAsync(TableDto value)
    {
        await Validate.FluentValidate(_validator, value);

        var table = _mapper.Map<Table>(value);

        var tableDto = await _repositories.TableRepository.InsertAsync(table);

        return _mapper.Map<TableDto>(tableDto);
    }

    public async Task<TableDto> SearchByIdAsync(int id)
    {
        var tableDto = await _repositories.TableRepository.SearchByIdAsync(id);

        return _mapper.Map<TableDto>(tableDto);
    }

    public async Task<TableDto> UpdateAsync(TableDto value)
    {
        var tableSearch = await _repositories.TableRepository.SearchByIdAsync(value.TableId);

        if (tableSearch is null)
            throw new ValidationException("Table does not exists");

        await Validate.FluentValidate(_validator, value);

        var table = await _repositories.TableRepository.UpdateAsync(_mapper.Map<Table>(value));

        return _mapper.Map<TableDto>(table);
    }
    #endregion
}

