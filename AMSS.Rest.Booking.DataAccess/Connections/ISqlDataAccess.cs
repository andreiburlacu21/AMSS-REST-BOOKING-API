using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Connections
{
    public interface ISqlDataAccess
    {
        string Connection { get; }
    }
}
