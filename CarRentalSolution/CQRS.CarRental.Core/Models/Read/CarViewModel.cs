using CQRS.CarRental.Core.Models.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Models.Read
{
    public class CarViewModel
    {
        public Guid CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double CurrentDistance { get; set; }
        public double TotalDistance { get; set; }
        public Status Status { get; set; }
    }
}
