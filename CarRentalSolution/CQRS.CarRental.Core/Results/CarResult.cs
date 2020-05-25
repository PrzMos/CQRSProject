using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Results
{
    public class CarResult : IResult
    {
        public Guid CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public Status Status { get; set; }
        public double  XPosition { get; set; }
        public double YPosition { get; set; }
    }
    public enum Status
    {
        wolny, wypożyczony
    }
}
