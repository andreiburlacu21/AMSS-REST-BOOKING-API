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
public class ServiceReviews : IServiceReviews
{
    #region Fields
    private readonly ISqlDataFactory _repositories;
    private readonly IValidator<ReviewDto> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ServiceReviews(ISqlDataFactory repositories, IValidator<ReviewDto> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods     
    public async Task<bool> DeleteAsync(ReviewDto value)
    {
        var review = _mapper.Map<Review>(value);

        return await _repositories.ReviewRepository.DeleteAsync(review);
    }

    public async Task<List<ReviewDto>> GetAllAsync()
    {
        var review = await _repositories.ReviewRepository.GetAllAsync();

        return _mapper.Map<List<ReviewDto>>(review);
    }

    public async Task<ReviewDto> InsertAsync(ReviewDto value)
    {
        var acc = await _repositories.ReviewRepository.FirstOrDefaultAsync(x => x.Description == value.Description &&
                                                                                x.AccountId == value.AccountId);

        if (acc is not null)
            throw new ValidationException("Review already exists");

        await Validate.FluentValidate(_validator, value);

        var review = _mapper.Map<Review>(value);

        var reviewDto = await _repositories.ReviewRepository.InsertAsync(review);

        return _mapper.Map<ReviewDto>(reviewDto);
    }

    public async Task<ReviewDto> SearchByIdAsync(int id)
    {
        var reviewDto = await _repositories.ReviewRepository.SearchByIdAsync(id);

        return _mapper.Map<ReviewDto>(reviewDto);
    }

    public async Task<ReviewDto> UpdateAsync(ReviewDto value)
    {
        var reviewSearch = await _repositories.ReviewRepository.SearchByIdAsync(value.ReviewId);

        if (reviewSearch is null)
            throw new ValidationException("Review does not exists");

        await Validate.FluentValidate(_validator, value);

        var review = await _repositories.ReviewRepository.UpdateAsync(_mapper.Map<Review>(value));

        return _mapper.Map<ReviewDto>(review);
    }
    #endregion
}

