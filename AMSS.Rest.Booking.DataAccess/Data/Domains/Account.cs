using AMSS.Rest.Booking.Utils.Enums;
using Dapper.Contrib.Extensions;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Accounts")]
public sealed class Account
{
    [ExplicitKey]
    public int AccountId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Role Role { get; set; }

}
