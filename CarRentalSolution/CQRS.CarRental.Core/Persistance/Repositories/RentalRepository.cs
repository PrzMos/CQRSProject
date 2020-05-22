using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Write;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(CarRentalContext carRentalContext) : base(carRentalContext)
        {
        }

        public Rental GetRentalWithDriverAndCarDetails(Guid id)
        {
           return _carRentalContext.Rentals.Where(x => x.RentalId == id).Include(x => x.Car).Include(x => x.Driver).FirstOrDefault();
        }
    }
}
