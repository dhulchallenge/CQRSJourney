using System;
using System.Collections.Generic;
using Booking.Command;
using Booking.Events;
using Infrastructure.Messaging;
using Infrastructure.Processes;
using Booking.Contract.Events;

namespace Booking
{
    public class BookingProcessManager  : IProcessManager
    {
        private readonly List<Envelope<ICommand>> commands = new List<Envelope<ICommand>>();

        public BookingProcessManager()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public bool Completed { get; private set; }
        public int CarId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }

        public IEnumerable<Envelope<ICommand>> Commands
        {
            get { return this.commands; }
        }

        public void Handle(BookingRequestPlaced @bookingRequestPlaced)
        {
            this.CarId = @bookingRequestPlaced.CarId;
            this.BookingStartDate = @bookingRequestPlaced.BookingStartDate;
            this.BookingEndDate = @bookingRequestPlaced.BookingEndDate;
            var makeBookingCommand = new MakeBooking(@bookingRequestPlaced.CarId) { BookingStartdate = @bookingRequestPlaced.BookingStartDate, BookingEndDate = @bookingRequestPlaced.BookingEndDate };
            makeBookingCommand.Id = @bookingRequestPlaced.SourceId;
            this.AddCommand(new Envelope<ICommand>(makeBookingCommand));
        }

        public void Handle(Envelope<BookingCreated> eBookingCreated)
        {
            var updateAvaliability = new UpdateAvailability(eBookingCreated.Body.carid);
        }

        public void Handle(CarFounded @carFounded)
        {
            var makeBooking = new MakeBooking(@carFounded.CarId) { BookingStartdate = @carFounded.BookingStartDate, BookingEndDate = @carFounded.BookingEndDate };
        }

        private void AddCommand<T>(T command)
           where T : ICommand
        {
            this.commands.Add(Envelope.Create<ICommand>(command));
        }

        private void AddCommand(Envelope<ICommand> envelope)
        {
            this.commands.Add(envelope);
        }
    }
}
