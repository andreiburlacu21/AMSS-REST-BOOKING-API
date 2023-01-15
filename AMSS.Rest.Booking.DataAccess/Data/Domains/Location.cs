using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Locations")]
public sealed class Location
{
    [ExplicitKey]
    public int LocationId { get; set; }
    public string? X { get; set; }
    public string? Y { get; set; }   
    public string? Address { get; set; }
}
