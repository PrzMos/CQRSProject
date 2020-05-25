using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries
{
    public class GetRentalByIdQuery : IQuery
    {
        public GetRentalByIdQuery(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public Guid RentalId { get; set; }
    }
}
