using System;
using System.Diagnostics;
using System.Runtime.Caching;
using Booking.Contract.Events;
using Booking.Readmodel;
using Infrastructure.EventSourcing;
using Infrastructure.Messaging.Handling;

namespace Booking.Handlers
{
    public class BookingViewModelGenerator : IEventHandler<BookingRequestPlaced>, IEventHandler<CarFounded>
    {
        private readonly Func<BookingDbContext> contextFactory;
        private readonly ObjectCache seatDescriptionsCache;

        public BookingViewModelGenerator(Func<BookingDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
            this.seatDescriptionsCache = MemoryCache.Default;
        }

        public void Handle(BookingRequestPlaced @event)
        {
            IBookingDao bookingDao = new BookingDao(contextFactory);
            if (bookingDao.IsCarAvailable(@event.CarId))
            {
                Trace.TraceWarning(
                        "EventId: {0} :The car is abvaliable for booking {1} as it was already created.",
                        @event.SourceId, @event.CarId);
            }
        }

        public void Handle(CarFounded @event)
        {
            IBookingDao bookingDao = new BookingDao(contextFactory);
            using (var repository = this.contextFactory.Invoke())
            {
                repository.Set<BookingDetails>().Add(
                        new BookingDetails(
                            @event.SourceId,
                            @event.CarId,
                            @event.BookingStartDate,
                            @event.BookingEndDate,
                            false,
                            DateTime.Now,
                            DateTime.Now));

                repository.SaveChanges();
            }
        }
    }
}
