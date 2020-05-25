using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Models.Read
{
    public class RentalReadModel
    {
        [Key]
        public Guid RentalId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public decimal Total { get; set; }
        public Guid DriverId { get; set; }
        public string Driver { get; set; }
        public Guid CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public double StartXPosition { get; set; }
        public double StartYPosition { get; set; }
        public double? StopXPosition { get; set; }
        public double? StopYPosition { get; set; }
    }
}
