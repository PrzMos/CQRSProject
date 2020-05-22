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
        public CarRentalContext Context { get; protected set; }
        public ICarRepository CarRepository { get; }

        public IDriverRepository DriverRepository { get; }

        public IDriverReadModelRepository DriverRead { get; }

        public ICarReadModelRepository CarReadModel { get; }

        public IRentalRepository RentalRepository { get; }

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
