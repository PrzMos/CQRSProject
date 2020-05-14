using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace CQRS.CarRental.Core.Models.Write
{
    public class Car
    {
        [Key]
        public Guid CarId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        public string RegistrationNumber { get; set; }
        [Required]
        public double XPosition { get; set; }
        [Required]
        public double YPosition { get; set; }
        [Required]
        public double CurrentDistance { get; set; }
        [Required]
        public double TotalDistance { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
