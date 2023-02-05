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

public class ServiceBookings : IServiceBookings
{
    #region Fields
    private readonly ISqlDataFactory _repositories;
    private readonly IValidator<BookingDto> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ServiceBookings(ISqlDataFactory repositories, IValidator<BookingDto> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods     
    public async Task<bool> DeleteAsync(BookingDto value)
    {
        var bookings = _mapper.Map<Bookings>(value);

        return await _repositories.BookingRepository.DeleteAsync(bookings);
    }

    public async Task<List<BookingDto>> GetAllAsync()
    {

        await this.BookingsToUpdateToCanceled();

        var bookings = await _repositories.BookingRepository.GetAllAsync();

        return _mapper.Map<List<BookingDto>>(bookings);
    }

    public async Task<BookingDto> InsertAsync(BookingDto value)
    {
        var acc = await _repositories.BookingRepository.FirstOrDefaultAsync(x => x.AccountId == value.AccountId && 
                                                                                 x.RestaurantId == value.RestaurantId &&
                                                                                 x.TableId == value.TableId &&
                                                                                 x.StartDate == value.StartDate);

        if (acc is not null)
            throw new ValidationException("Booking already exists");

        await Validate.FluentValidate(_validator, value);

        var booking = _mapper.Map<Bookings>(value);

        booking.Status = Utils.Enums.Status.Created;

        var bookingDto = await _repositories.BookingRepository.InsertAsync(booking);

        return _mapper.Map<BookingDto>(bookingDto);
    }

    public async Task<BookingDto> SearchByIdAsync(int id)
    {
        var bookingsDto = await _repositories.BookingRepository.SearchByIdAsync(id);

        return _mapper.Map<BookingDto>(bookingsDto);
    }

    public async Task<BookingDto> UpdateAsync(BookingDto value)
    {
        var bookingsSearch = await _repositories.BookingRepository.SearchByIdAsync(value.BookingId);

        if (bookingsSearch is null)
            throw new ValidationException("Booking does not exists");

        await Validate.FluentValidate(_validator, value);

        var booking = await _repositories.BookingRepository.UpdateAsync(_mapper.Map<Bookings>(value));

        return _mapper.Map<BookingDto>(booking);
    }
    #endregion


    private async Task BookingsToUpdateToCanceled()
    {
        var dateNow = DateTime.Now;

        var bookingsToUpdate = await _repositories.BookingRepository.GetEntitiesWhereAsync(booking =>
        {

            if (DateTime.TryParse(booking.StartDate, out DateTime dateOut))
            {
                var resultDate = dateOut - dateNow;

                if (dateOut.Hour > 0 && dateOut.Hour < 24) 
                    return true;
            }
            return false;
        });

        bookingsToUpdate.ForEach(async booking =>
        {
            booking.Status = Utils.Enums.Status.Canceled;

            await _repositories.BookingRepository.UpdateAsync(booking);
        });

    }
}
