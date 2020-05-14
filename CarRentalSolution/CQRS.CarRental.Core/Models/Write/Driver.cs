using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Models.Write
{
    public class Driver
    {
        [Key]
        public Guid DriverId { get; set; }
        [MaxLength(14)]
        [Required]
        public string LicenceNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public Driver(string licenceNumber, string firstName, string lastName)
        {
            DriverId = Guid.NewGuid();
            LicenceNumber = licenceNumber;
            FirstName = firstName;
            LicenceNumber = lastName;
        }
    }
}
