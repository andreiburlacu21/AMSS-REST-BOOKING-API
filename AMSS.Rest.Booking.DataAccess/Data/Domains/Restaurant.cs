using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Restaurants")]
public sealed class Restaurant
{
    [ExplicitKey]
    public int RestaurantId { get; set; }
    public string? Name { get; set; }
    public int LocationId { get; set; }
    public double Rating { get; set; }
    public string? Description { get; set; }
}
