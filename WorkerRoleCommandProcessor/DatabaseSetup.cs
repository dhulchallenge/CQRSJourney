using Booking.Database;
using Booking.Readmodel;
using Conference.Common.Entity;

namespace WorkerRoleCommandProcessor
{
    using System.Data.Entity;
    using Infrastructure.Sql.BlobStorage;
    using Infrastructure.Sql.EventSourcing;
    using Infrastructure.Sql.MessageLog;

    /// <summary>
    /// Initializes the EF infrastructure.
    /// </summary>
    internal static class DatabaseSetup
    {
        public static void Initialize()
        {
            Database.DefaultConnectionFactory = new ServiceConfigurationSettingConnectionFactory(Database.DefaultConnectionFactory);

            Database.SetInitializer<BookingDbContext>(null);
            Database.SetInitializer<BookingProcessManagerDbContext>(null);
            Database.SetInitializer<EventStoreDbContext>(null);
            Database.SetInitializer<MessageLogDbContext>(null);
            Database.SetInitializer<BlobStorageDbContext>(null);

        }
    }
}