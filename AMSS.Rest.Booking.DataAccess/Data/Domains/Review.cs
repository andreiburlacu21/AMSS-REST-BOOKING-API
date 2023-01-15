using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Reviews")]
public sealed class Review
{
    [ExplicitKey]
    public int ReviewId { get; set; }
    public int BookingId { get; set; }
    public int Grade { get; set; }
    public string? Description { get; set; }
}
