using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Data.Domains;

[Table("table_Menus")]
public sealed class Menu
{
    [ExplicitKey]
    public int MenuId { get; set; }
    public int RestaurantId { get; set; }
    public string? Content { get; set; }
}
