using Booking.Command;
using Infrastructure.EventSourcing;
using Infrastructure.Messaging.Handling;

namespace Booking.Handlers
{
    public class BookCarHandler : ICommandHandler<BookCar>, 
        ICommandHandler<CarAvailabiltyCommand>,
        ICommandHandler<MakeBooking>
    {
        private readonly IEventSourcedRepository<Booking> repository;
        public BookCarHandler(IEventSourcedRepository<Booking> repository)
        {
            this.repository = repository;
        }

        public void Handle(BookCar command)
        {
            var bookcar = repository.Find(command.BookingId);
            if (bookcar == null)
            {
                bookcar = new Booking(command.BookingId,command.CarId,command.BookingStartDate,command.BookingEndDate) ;
            }
            repository.Save(bookcar, command.Id.ToString());
        }

        public void Handle(CarAvailabiltyCommand carAvailable)
        {
            var availability = this.repository.Get(carAvailable.Id);
            availability.AssignCar(carAvailable.CarId);
            this.repository.Save(availability, carAvailable.Id.ToString());
        }

        public void Handle(MakeBooking command)
        {
            var availability = this.repository.Get(command.Id);
            availability.MakeBooking(command.Id, command.CarId, command.BookingStartdate, command.BookingEndDate);
            this.repository.Save(availability, command.Id.ToString());
        }
    }
}
