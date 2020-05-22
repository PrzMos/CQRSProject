using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class CreateRentCommand : ICommand
    {
        public DateTime Started { get; set; }
        public Guid DriverId { get; set; }
        public Guid CarId { get; set; }
    }
}
