using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Readmodel
{
    public interface IBookingDao
    {
        bool IsCarAvailable(int CarId );
        BookingDetails GetBookingDetails(Guid bookingId);

    }
}
