using CQRS.CarRental.Core.Models.Write;
using SharedKernel.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
    }
}
