using AMSS.Rest.Booking.Utils.Enums;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Bookings")]
public sealed class Bookings
{
    [ExplicitKey]
    public int BookingId { get; set; }
    public int RestaurantId { get; set; }
    public int TableId { get; set; }
    public int AccountId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set;}
    public Status Status { get; set; }
    public bool IsReviewed { get; set; }
}
