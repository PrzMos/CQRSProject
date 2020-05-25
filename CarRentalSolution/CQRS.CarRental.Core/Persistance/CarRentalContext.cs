using CQRS.CarRental.Core.Models.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using CQRS.CarRental.Core.Models.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
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
        public DbSet<CarViewModel> CarViewModels{ get; set; }
        public DbSet<DriverViewModel> DriverViewModels{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public void InitialDbData()
        {
            if (!Cars.Any())
            {
                List<Vehicle> cars = new List<Vehicle>()
                {
                    new Car(){ CarId = Guid.Parse("7295a624-dea8-479c-b920-a3254f92af77"), CurrentDistance = 20.3, RegistrationNumber="KRA2436", Status = Status.wolny, TotalDistance = 15034.5, XPosition = 2.4, YPosition = 5.2 },
                    new Car(){ CarId = Guid.Parse("6291ffd4-cb73-4c37-aa9a-1591247f554d"), CurrentDistance = 13.7, RegistrationNumber="KOL0201", Status = Status.wypożyczony, TotalDistance = 20134.5, XPosition = 1.4, YPosition = -2.2 },
                    new Car(){ CarId = Guid.Parse("7fb934b4-dae2-4659-a9ee-d5af7af36f40"), CurrentDistance = 20.3, RegistrationNumber="KRA2436", Status = Status.wolny, TotalDistance = 15034.5, XPosition = 2.4, YPosition = 5.2 }
                };
                foreach (Car item in cars)
                {
                    Cars.Add(item);
                    var carReadModel = new CarViewModel()
                    {
                        CarId = item.CarId,
                        CurrentDistance = item.CurrentDistance,
                        RegistrationNumber = item.RegistrationNumber,
                        Status = item.Status,
                        TotalDistance = item.TotalDistance,
                        XPosition = item.XPosition,
                        YPosition = item.YPosition
                    };
                    CarViewModels.Add(carReadModel);
                }
            }

            if (!Drivers.Any())
            {
                Driver driver = new Driver("42222/12/01118", "Jan", "Kowalski");
                driver.DriverId = Guid.Parse("0871afd6-dae6-45f2-b9cf-8f91f184d6af");

                Drivers.Add(driver);

                DriverViewModel driverViewModel = new DriverViewModel()
                {
                    DriverId = driver.DriverId,
                    FirstName = driver.FirstName,
                    LastName = driver.LastName,
                    LicenceNumber = driver.LicenceNumber
                };

                DriverViewModels.Add(driverViewModel);
            }

            if (!Rentals.Any())
            {
                Rental rental = new Rental()
                {
                    RentalId = Guid.Parse("1c3444e1-e09c-48a1-9cf2-db713731b5b1"),
                    CarId = Guid.Parse("6291ffd4-cb73-4c37-aa9a-1591247f554d"),
                    Started = DateTime.ParseExact("21/05/2020 07:25:47","dd/MM/yyyy hh:mm:ss",CultureInfo.InvariantCulture),
                    DriverId = Guid.Parse("0871afd6-dae6-45f2-b9cf-8f91f184d6af")
                };

                Rentals.Add(rental);

                RentalReadModel rentalReadModel = new RentalReadModel()
                {
                    RentalId = rental.RentalId,
                    RegistrationNumber = "KOL0201",
                    CarId = rental.CarId,
                    Created = rental.Started,
                    StartXPosition = 1.4,
                    StartYPosition = -2.2
                };

                RentalReadModels.Add(rentalReadModel);
            }

            this.SaveChanges();
        }
    }
}
