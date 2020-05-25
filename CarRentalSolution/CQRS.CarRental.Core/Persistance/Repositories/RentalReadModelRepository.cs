using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using SharedKernel.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class RentalReadModelRepository : Repository<RentalReadModel>, IRentalReadModelRepository
    {
        public RentalReadModelRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }
    }
}
