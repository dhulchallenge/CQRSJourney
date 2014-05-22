using System;
using Infrastructure.Messaging;

namespace Booking.Command
{
    public abstract class CarAvailabiltyCommand : ICommand, IMessageSessionProvider
    {
        public CarAvailabiltyCommand(int carId)
        {
            this.Id = Guid.NewGuid();
            CarId = carId;
        }

        public Guid Id { get; set; }
        public int CarId { get; set; }

        string IMessageSessionProvider.SessionId
        {
            get { return this.SessionId; }
        }

        protected string SessionId
        {
            get { return "CarAvailability_" + this.CarId.ToString(); }
        }
    }
}
