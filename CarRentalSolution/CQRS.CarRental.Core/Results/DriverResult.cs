using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Results
{
    public class DriverResult : IResult
    {
        public Guid DriverId { get; set; }
        [MaxLength(14)]
        [Required]
        public string LicenceNumber { get; set; }
        public string FullName { get; set; }
    }
}
