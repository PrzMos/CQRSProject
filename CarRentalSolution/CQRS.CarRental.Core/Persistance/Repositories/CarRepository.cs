using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }
    }
}
