using CQRS.CarRental.Core.Models.Write;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Models.Interfaces
{
    public abstract class Vehicle
    {
        [Key]
        public Guid CarId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        public string RegistrationNumber { get; set; }
        [Required]
        public double CurrentDistance { get; set; }
        [Required]
        public double TotalDistance { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public double XPosition { get; set; }
        [Required]
        public double YPosition { get; set; }
        public virtual void UpdatePositions(double stopX, double stopY)
        {
            XPosition = stopX;
            YPosition = stopY;
        }
        public void ChangeStatus()
        {
            if (Status == Status.wolny)
            {
                Status = Status.wypożyczony;
            }
            else
            {
                Status = Status.wolny;
            }
        }
    }
}
