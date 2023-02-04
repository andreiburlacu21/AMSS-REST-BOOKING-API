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

namespace AMSS.Rest.Booking.Service.Model
{
    public class ServiceRestaurants : IServiceRestaurants
    {
        #region Fields
        private readonly ISqlDataFactory _repositories;
        private readonly IValidator<RestaurantDto> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ServiceRestaurants(ISqlDataFactory repositories, IValidator<RestaurantDto> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(RestaurantDto value)
        {
            var restaurants = _mapper.Map<Restaurant>(value);

            return await _repositories.RestaurantRepository.DeleteAsync(restaurants);
        }

        public async Task<List<RestaurantDto>> GetAllAsync()
        {
            var restaurants = await _repositories.RestaurantRepository.GetAllAsync();

            return _mapper.Map<List<RestaurantDto>>(restaurants);
        }

        public async Task<RestaurantEntityDto> GetInfoRestaurant(int resturantId)
        {
            var restaurant = await _repositories.RestaurantRepository.FirstOrDefaultAsync(x => x.RestaurantId == resturantId) ?? 
                             throw new ValidationException("Restaurant does not exists");

            var listOfMenus = await _repositories.MenuRepository.GetEntitiesWhereAsync(x => x.RestaurantId.Equals(resturantId));

            var listOfTables = await _repositories.TableRepository.GetEntitiesWhereAsync(x => x.RestauranId.Equals(resturantId));

            var listOfBookings = await _repositories.BookingRepository.GetEntitiesWhereAsync(x => x.RestaurantId.Equals(resturantId));

            var location = await _repositories.LocationRepository.FirstOrDefaultAsync(x => x.LocationId == restaurant.LocationId);

            var listOfReviews = await _repositories.ReviewRepository.GetEntitiesWhereAsync(booking =>
            {
                var flag = false;

                if (listOfBookings.Any(x => x.BookingId == booking.BookingId))
                {
                    flag = true;
                }
                return flag;
            });

            var dto = new RestaurantEntityDto()
            {
                Reviews = _mapper.Map<List<ReviewDto>>(listOfReviews),
                Menus = _mapper.Map<List<MenuDto>>(listOfMenus),
                Bookings = _mapper.Map<List<BookingDto>>(listOfBookings),
                Tables = _mapper.Map<List<TableDto>>(listOfTables),
                Location = _mapper.Map<LocationDto>(location),
            };

            return dto;
        }

        public async Task<RestaurantDto> InsertAsync(RestaurantDto value)
        {
            var acc = await _repositories.RestaurantRepository.FirstOrDefaultAsync(x => x.Name == value.Name &&
                                                                                        x.LocationId == value.LocationId);

            if (acc is not null)
                throw new ValidationException("Restaurant already exists");

            await Validate.FluentValidate(_validator, value);

            var restaurant = _mapper.Map<Restaurant>(value);

            var restaurantDto = await _repositories.RestaurantRepository.InsertAsync(restaurant);

            return _mapper.Map<RestaurantDto>(restaurantDto);
        }

        public async Task<RestaurantDto> SearchByIdAsync(int id)
        {
            var restaurantsDto = await _repositories.RestaurantRepository.SearchByIdAsync(id);

            return _mapper.Map<RestaurantDto>(restaurantsDto);
        }

        public async Task<RestaurantDto> UpdateAsync(RestaurantDto value)
        {
            var restaurantsSearch = await _repositories.RestaurantRepository.SearchByIdAsync(value.RestaurantId);

            if (restaurantsSearch is null)
                throw new ValidationException("Restaurant does not exists");

            await Validate.FluentValidate(_validator, value);

            var restaurant = await _repositories.RestaurantRepository.UpdateAsync(_mapper.Map<Restaurant>(value));

            return _mapper.Map<RestaurantDto>(restaurant);
        }
        #endregion
    }

}
