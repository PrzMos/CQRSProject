using CQRS.CarRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CQRS.CarRental.Core.Persistance
{
    public class CarRentalUnitOfWork : ICarRentalUnitOfWork
    {
        public CarRentalUnitOfWork(CarRentalContext context, ICarRepository carRepository, IDriverRepository driverRepository, IDriverReadModelRepository driverRead, ICarReadModelRepository carReadModel, IRentalRepository rentalRepository, IRentalReadModelRepository rentalReadModel)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            CarRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
            DriverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
            DriverRead = driverRead ?? throw new ArgumentNullException(nameof(driverRead));
            CarReadModel = carReadModel ?? throw new ArgumentNullException(nameof(carReadModel));
            RentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            RentalReadModel = rentalReadModel ?? throw new ArgumentNullException(nameof(rentalReadModel));
        }

        public CarRentalContext Context { get; protected set; }
        public ICarRepository CarRepository { get; }

        public IDriverRepository DriverRepository { get; }

        public IDriverReadModelRepository DriverRead { get; }

        public ICarReadModelRepository CarReadModel { get; }

        public IRentalRepository RentalRepository { get; }

        public IRentalReadModelRepository RentalReadModel { get; }
        

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in Context.ChangeTracker.Entries().Where(x=>x.State == Microsoft.EntityFrameworkCore.EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    case Microsoft.EntityFrameworkCore.EntityState.Detached:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
