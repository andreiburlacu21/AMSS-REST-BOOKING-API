using AMSS.Rest.Booking.DataAccess.Data.Domains;
using AMSS.Rest.Booking.DataAccess.Factory;
using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Model.Contracts;
using AMSS.Rest.Booking.Utils.Enums;
using AMSS.Rest.Booking.Validator;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Service.Model;

public class ServiceAccounts : IServiceAccounts
{
    #region Fields
    private readonly ISqlDataFactory _repositories;
    private readonly IValidator<AccountDto> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ServiceAccounts(ISqlDataFactory repositories, IValidator<AccountDto> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods     
    public async Task<bool> DeleteAsync(AccountDto value)
    {
        var accounts = _mapper.Map<Account>(value);

        return await _repositories.AccountsRepository.DeleteAsync(accounts);
    }

    public async Task<List<AccountDto>> GetAllAsync()
    {
        var accounts = await _repositories.AccountsRepository.GetAllAsync();

        // if (!accounts.Any()) throw new ValidationException("This table is empty");

        return _mapper.Map<List<AccountDto>>(accounts);
    }

    public async Task<AccountDto> InsertAsync(AccountDto value)
    {
        var acc = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName.Equals(value.UserName));

        if (acc is not null)
            throw new ValidationException("Account already exists");

        await Validate.FluentValidate(_validator, value);

        var account = _mapper.Map<Account>(value);

        account.Role = Role.User;

        var accountDto = await _repositories.AccountsRepository.InsertAsync(account);

        return _mapper.Map<AccountDto>(accountDto);
    }

    public async Task<AccountDto> SearchByIdAsync(int id)
    {
        var accountsDto = await _repositories.AccountsRepository.SearchByIdAsync(id);

        return _mapper.Map<AccountDto>(accountsDto);
    }

    public async Task<AccountDto> UpdateAsync(AccountDto value)
    {
        if (await _repositories.AccountsRepository.SearchByIdAsync(value.AccountId) is null)
            throw new ValidationException("Account does not exists");

        var accountsSearch = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == value.UserName);

        if (accountsSearch is not null && value.AccountId != accountsSearch.AccountId)
            throw new ValidationException("This Username is taken");
        await Validate.FluentValidate(_validator, value);

        var account = await _repositories.AccountsRepository.UpdateAsync(_mapper.Map<Account>(value));

        return _mapper.Map<AccountDto>(account);
    }
    #endregion

    public async Task<AccountDto> SearchByEmailAsync(string email)
    {
        var accountsDto = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.Email == email);

        return _mapper.Map<AccountDto>(accountsDto);
    }

    public async Task<AccountDto> SearchByUserNameAsync(string userName)
    {
        var accountsDto = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == userName);

        return _mapper.Map<AccountDto>(accountsDto);
    }
}
