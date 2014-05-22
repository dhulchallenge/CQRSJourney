using System;
using System.Diagnostics;
using Booking.Contract.Events;
using Booking.Events;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Handling;
using Infrastructure.Processes;

namespace Booking
{
    public class BookingProcessManagerRouter : 
        IEventHandler<BookingRequestPlaced>, 
        IEnvelopedEventHandler<BookingCreated>
    {
         private readonly Func<IProcessManagerDataContext<BookingProcessManager>> contextFactory;

         public BookingProcessManagerRouter(Func<IProcessManagerDataContext<BookingProcessManager>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

         public void Handle(BookingRequestPlaced @event)
         {
             using (var context = this.contextFactory.Invoke())
             {
                 var pm = context.Find(x => x.Id == @event.SourceId);
                 if (pm == null)
                 {
                     pm = new BookingProcessManager();
                 }

                 pm.Handle(@event);
                 context.Save(pm);
             }
         }

        //public void Handle(CarFounded @event)
        //{
        //    using (var context = this.contextFactory.Invoke())
        //    {
        //        var pm = context.Find(x => x.Id == @event.SourceId);
        //        if (pm == null)
        //        {
        //            pm = new BookingProcessManager();
        //        }
        //        pm.Handle(@event);
        //        context.Save(pm);
        //    }
        //}



        public void Handle(Envelope<BookingCreated> envelope)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var pm = context.Find(x => x.Id == envelope.Body.SourceId);
                if (pm != null)
                {
                    pm.Handle(envelope);

                    context.Save(pm);
                }
                else
                {
                    // TODO: should Cancel seat reservation!
                    Trace.TraceError("Failed to locate the registration process manager handling the seat reservation with id {0}. TODO: should Cancel seat reservation!", envelope.Body.carid);
                }
            }
        }
    }
}
