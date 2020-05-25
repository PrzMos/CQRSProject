using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Models.Read
{
    public class DriverViewModel
    {
        [Key]
        public Guid DriverId { get; set; }
        public string LicenceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
