using CQRS.CarRental.Core.Models.Write;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Results
{
    public class RentalModelResult : RentalResult, IResult
    {
        public Guid DriverId { get; set; }
        public string Driver { get; set; }
        public Guid CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public double StartXPosition { get; set; }
        public double StartYPosition { get; set; }
        public double StopXPosition { get; set; }
        public double StopYPosition { get; set; }
    }
}
