using SharedKernel.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Interfaces
{
    public interface ICarRentalUnitOfWork : IUnitOfWork
    {
        ICarRepository CarRepository { get; }
        IDriverRepository DriverRepository { get; }
        IDriverReadModelRepository DriverRead { get; }
        ICarReadModelRepository CarReadModel { get; }
        IRentalRepository RentalRepository { get; }
        IRentalReadModelRepository RentalReadModel { get; }
    }
}
