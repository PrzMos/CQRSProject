using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Results
{
    public class RentalResult : IResult
    {
        public Guid RentalId { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public decimal Total { get; set; }
    }
}
