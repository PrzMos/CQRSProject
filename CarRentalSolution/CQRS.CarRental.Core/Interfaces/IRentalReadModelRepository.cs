using CQRS.CarRental.Core.Models.Read;
using SharedKernel.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Interfaces
{
    public interface IRentalReadModelRepository : IRepository<RentalReadModel>
    {

    }
}
