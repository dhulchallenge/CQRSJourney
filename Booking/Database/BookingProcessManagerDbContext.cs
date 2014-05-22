using System.Data.Entity;
using Infrastructure.Sql.Processes;
namespace Booking.Database
{
    public class BookingProcessManagerDbContext : DbContext
    {
        public const string SchemaName = "CarRentals";

         public BookingProcessManagerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookingProcessManager>().ToTable("BookingProcess", SchemaName);
            modelBuilder.Entity<UndispatchedMessages>().ToTable("UndispatchedMessages", SchemaName);
        }

        // Define the available entity sets for the database.
        public DbSet<BookingProcessManager> BookingProcess { get; set; }
    }
}
