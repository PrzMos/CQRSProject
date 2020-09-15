using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class CreateRentCommand : ICommand
    {
        [DataType(DataType.DateTime)]
        public DateTime Started { get; set; }
        [Required]
        public Guid DriverId { get; set; }
        [Required]
        public Guid CarId { get; set; }
    }
}
