using CQRS.CarRental.Core.Models.Write;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class CreateCarCommand : ICommand
    {
        [Required]
        [StringLength(7,MinimumLength = 7)]
        public string RegistrationNumber { get; set; }
        [Required]
        public double XPosition { get; set; }
        [Required]
        public double YPosition { get; set; }
        [Required]
        public double CurrentDistance { get; set; }
        [Required]
        public double TotalDistance { get; set; }
        public StatusCommand Status { get; set; } = StatusCommand.wolny;

    }

    public enum StatusCommand
    {
        wolny, wypożyczony
    }
}
