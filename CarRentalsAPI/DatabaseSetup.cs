using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarRentals;
using Conference.Common.Entity;
using Infrastructure.Sql.BlobStorage;

namespace CarRentalsAPI
{
    internal static class DatabaseSetup
    {
        public static void Initialize()
        {
            Database.DefaultConnectionFactory = new ServiceConfigurationSettingConnectionFactory(Database.DefaultConnectionFactory);

            Database.SetInitializer<BlobStorageDbContext>(null);
            Database.SetInitializer<CarRenatlsContext>(null);
        }
    }
}