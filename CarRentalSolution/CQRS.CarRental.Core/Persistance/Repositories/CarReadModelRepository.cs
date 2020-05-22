using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class CarReadModelRepository : Repository<CarViewModel>, ICarReadModelRepository
    {
        public CarReadModelRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }
    }
}
