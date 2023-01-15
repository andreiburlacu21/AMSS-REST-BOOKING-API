using AMSS.Rest.Booking.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DTO;

public sealed class BookingDto
{
    public int BookingId { get; set; }
    public int RestaurantId { get; set; }
    public int TableId { get; set; }
    public int AccountId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public Status Status { get; set; }
    public bool IsReviewed { get; set; }
}
