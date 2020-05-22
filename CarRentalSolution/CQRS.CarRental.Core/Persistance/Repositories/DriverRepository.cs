using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }
    }
}
