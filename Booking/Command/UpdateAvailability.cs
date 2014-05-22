using System;
using Infrastructure.Messaging;

namespace Booking.Command
{
    public class UpdateAvailability : ICommand, IMessageSessionProvider
    {
        public UpdateAvailability(int CarId)
        {
            Id = Guid.NewGuid();
            this.CarId = CarId;
        }
        public int CarId { get; set; }
        public Guid Id { get; private set; }
        public string SessionId { get; private set; }
    }
}
