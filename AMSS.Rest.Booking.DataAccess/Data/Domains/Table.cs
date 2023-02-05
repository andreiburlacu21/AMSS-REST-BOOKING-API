using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Tables")]
public sealed class Table
{
    [ExplicitKey]
    public int TableId { get; set; }
    public int RestaurantId { get; set; }
    public int NumberOfSeats { get; set; }
    public bool Outdoor { get; set; }

}
