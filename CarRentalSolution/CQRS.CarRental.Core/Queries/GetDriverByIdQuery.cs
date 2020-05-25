using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries
{
    public class GetDriverByIdQuery : IQuery
    {
        public GetDriverByIdQuery(Guid driverId)
        {
            DriverId = driverId;
        }

        public Guid DriverId { get; set; }
    }
}
