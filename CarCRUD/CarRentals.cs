using System.Data.Entity;

namespace CarRentals
{
    public class CarRenatlsContext : DbContext
    {
        public const string SchemaName = "CarRentals";

        public CarRenatlsContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }

    }
}
