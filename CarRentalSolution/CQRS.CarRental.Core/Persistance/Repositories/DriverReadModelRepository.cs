using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class DriverReadModelRepository : Repository<DriverViewModel>, IDriverReadModelRepository
    {
        public DriverReadModelRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }
    }
}
