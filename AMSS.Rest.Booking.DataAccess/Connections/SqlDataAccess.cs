using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.DataAccess.Connections
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public string Connection { get; private set; }
        public SqlDataAccess(string connection)
        {
            Connection = connection;
        }
    }
}
