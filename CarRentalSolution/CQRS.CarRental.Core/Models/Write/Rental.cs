using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CQRS.CarRental.Core.Models.Write
{
    public class Rental
    {
        [Key]
        public Guid RentalId { get; set; }
        [Required]
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public decimal Total { get; set; }
    }
}
