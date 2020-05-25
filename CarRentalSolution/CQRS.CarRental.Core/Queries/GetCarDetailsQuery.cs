using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries
{
    public class GetCarDetailsQuery : IQuery
    {
        public GetCarDetailsQuery(Guid carId)
        {
            CarId = carId;
        }

        public Guid CarId { get; set; }
    }
}
