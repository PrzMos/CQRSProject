using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using CQRS.CarRental.Core.Models.Write;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class RentalReadModelRepository : Repository<RentalReadModel>, IRentalReadModelRepository
    {
        public RentalReadModelRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }

        public RentalReadModel GetRentalWithCarAndDriver(Guid rentalId)
        {
            return _carRentalContext.RentalReadModels.Where(x => x.RentalId == rentalId).Include(x => x.Car).FirstOrDefault();
        }
    }
}
