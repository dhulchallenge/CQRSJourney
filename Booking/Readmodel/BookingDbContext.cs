using System;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Booking.Contract;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using Microsoft.Practices.TransientFaultHandling;

namespace Booking.Readmodel
{
    public class BookingDbContext:DbContext
    {
         public const string SchemaName = "CarRentals";
        private readonly RetryPolicy<SqlAzureTransientErrorDetectionStrategy> retryPolicy;

        public BookingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(new Incremental(3, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1.5)) { FastFirstRetry = true });
            this.retryPolicy.Retrying += (s, e) =>
                Trace.TraceWarning("An error occurred in attempt number {1} to access the BookingDbContext: {0}", e.LastException.Message, e.CurrentRetryCount);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Make the name of the views match exactly the name of the corresponding property.
            modelBuilder.Entity<BookingDetails>().ToTable("Bookings", SchemaName);
            modelBuilder.Entity<CarsAvailable>().ToTable("Availability",SchemaName);
        }

        public T Find<T>(Guid id) where T : class
        {
            return this.retryPolicy.ExecuteAction(() => this.Set<T>().Find(id));
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Save<T>(T entity) where T : class
        {
            var entry = this.Entry(entity);

            if (entry.State == EntityState.Detached)
                this.Set<T>().Add(entity);

            this.retryPolicy.ExecuteAction(() => this.SaveChanges());
        }

    }
}
