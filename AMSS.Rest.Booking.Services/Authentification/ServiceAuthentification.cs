using AMSS.Rest.Booking.DataAccess.Data.Domains;
using AMSS.Rest.Booking.DataAccess.Factory;
using AMSS.Rest.Booking.DTO;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AMSS.Rest.Booking.Validator.Model.Validation;
using AMSS.Rest.Booking.Validator;
using AMSS.Rest.Booking.Utils.Enums;

namespace AMSS.Rest.Booking.Service.Authentification;

public class ServiceAuthentification : IServiceAuthentification
{
    private readonly ISqlDataFactory _repositories;
    private readonly string _myKey;
    private readonly IMapper _mapper;

    public ServiceAuthentification(ISqlDataFactory repositories, string myKey, IMapper mapper)
    {
        _myKey = myKey;
        _repositories = repositories;
        _mapper = mapper;

    }
    private async Task<bool> CheckEmailAsync(string email)
    {
        return await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.Email == email) != null;

    }

    private async Task<bool> CheckUserNameAsync(string userName)
    {
        return await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == userName) != null;
    }

    public async Task<dynamic> GenerateTokenAsync(AuthDto authData)
    {
        await IsValidUserNameAndPassowrdAsync(authData);
        var username = authData.UserName;
        var user = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == username);

        var claims = new List<Claim>
            {
                new Claim("Identifier", user.AccountId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())
            };

        var token = new JwtSecurityToken
            (
               new JwtHeader
                (
                    new SigningCredentials
                    (
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_myKey)),
                        SecurityAlgorithms.HmacSha256
                    )
                 ),
                new JwtPayload(claims)
            );
        var output = new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            user.UserName
        };

        return output;
    }


    public async Task<bool> IsValidUserNameAndPassowrdAsync(AuthDto authData)
    {
        var user = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == authData.UserName) ??
                       throw new ValidationException("Invalid username or password");

        if (user.Password != authData.Password)
            throw new ValidationException("Invalid username or password");
        return true;
    }

    public async Task<AccountDto> RegisterAsync(AccountDto user)
    {
        if (await CheckEmailAsync(user.Email))
            throw new ValidationException("Invalid email");

        if (await CheckUserNameAsync(user.UserName))
            throw new ValidationException("Invalid username");
        user.Role = Role.User;

        var validator = new AccountValidation();
        await Validate.FluentValidate(validator, user);

        var result = _mapper.Map<Account>(user);

        return _mapper.Map<AccountDto>(await _repositories.AccountsRepository.InsertAsync(result));

    }
}
