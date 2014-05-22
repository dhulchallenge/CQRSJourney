using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Command;
using Booking.Contract;
using Booking.Contract.Events;
using Infrastructure.EventSourcing;
using Booking.Events;

namespace Booking
{
    public class CarAvailability : EventSourced, IMementoOriginator
    {
        private readonly Dictionary<int, int> availableCars = new Dictionary<int, int>();
        private readonly Dictionary<int, CarsAvailable> pendingBookings = new Dictionary<int, CarsAvailable>();
        
        public CarAvailability(Guid id) : base(id)
        {
            base.Handles<AvailabilityCarsChanged>(this.OnAvailableCarsChanged);
        }


        public CarAvailability(Guid Id, IMemento memento, IEnumerable<VersionedEvent> history) : base(Id)
        {
            var state = (Memento)memento;
            this.Version = state.Version;
            // make a copy of the state values to avoid concurrency problems with reusing references.
            this.availableCars.AddRange(state.AvailableCars);
            this.pendingBookings.AddRange(state.PendingBookings);
            this.LoadFrom(history);
        }

        public void AddSeats(int CarId)
        {
            base.Update(new AvailabilityCarsChanged{ AvailableCars = new[] { new CarsAvailable(CarId,1) }});
        }

        public void RemoveSeats(int CarId)
        {
            base.Update(new AvailabilityCarsChanged { AvailableCars = new[] { new CarsAvailable(CarId, 1) } });
        }




        //private void OnBookingsCancelled(SeatsReservationCancelled e)
        //{
        //    foreach (var seat in e.AvailableSeatsChanged)
        //    {
        //        this.remainingSeats[seat.SeatType] = this.remainingSeats[seat.SeatType] + seat.Quantity;
        //    }
        //}

        public void UpdateCarAvailable(int CarId)
        {
            foreach (var cars in availableCars)
            {
                int newValue = cars.Value-1;
                int remaining;
                if (this.availableCars.TryGetValue(CarId, out remaining))
                {
                    newValue += remaining;
                }

                this.availableCars[CarId] = newValue;
            }
        }
        private void OnAvailableCarsChanged(AvailabilityCarsChanged e)
        {
            foreach (var cars in e.AvailableCars)
            {
                int newValue = cars.Count;
                int remaining;
                if (this.availableCars.TryGetValue(cars.CarId, out remaining))
                {
                    newValue += remaining;
                }

                this.availableCars[cars.CarId] = newValue;
            }
        }
      
        private static TValue GetOrAdd<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = new TValue();
                dictionary[key] = value;
            }

            return value;
        }

        /// <summary>
        /// Saves the object's state to an opaque memento object (a snapshot) that can be used to restore the state by using the constructor overload.
        /// </summary>
        /// <returns>An opaque memento object that can be used to restore the state.</returns>
        public IMemento SaveToMemento()
        {
            return new Memento
            {
                Version = this.Version,
                AvailableCars = this.availableCars.ToArray(),
                PendingBookings = this.pendingBookings,
            };
        }

        internal class Memento : IMemento
        {
            public int Version { get; internal set; }
            internal KeyValuePair<int, int>[] AvailableCars { get; set; }
            internal Dictionary<int, CarsAvailable> PendingBookings { get; set; }
        }
    }
}
