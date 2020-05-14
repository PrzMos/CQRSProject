using CQRS.CarRental.Core.Models.Read;
using CQRS.CarRental.Core.Models.Write;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CQRS.CarRental.Core.Persistance
{
    public class CarRentalContext :DbContext
    {
        public string ConnectionString { get; } = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CQRS_EscapeRoom;Trusted_Connection=True";


        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<RentalReadModel> RentalReadModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
