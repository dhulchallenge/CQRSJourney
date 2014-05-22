using Booking.Command;
using Infrastructure.EventSourcing;
using Infrastructure.Messaging.Handling;

namespace Booking.Handlers
{
    public class UpdateAvailablityHandler : ICommandHandler<UpdateAvailability>
    {
        private readonly IEventSourcedRepository<CarAvailability> repository;
        public UpdateAvailablityHandler(IEventSourcedRepository<CarAvailability> repository)
        {
            this.repository = repository;
        }
        public void Handle(UpdateAvailability command)
        {
            var availability = this.repository.Get(command.Id);
            availability.UpdateCarAvailable(command.CarId);
            this.repository.Save(availability, command.Id.ToString());
        }
    }
}
