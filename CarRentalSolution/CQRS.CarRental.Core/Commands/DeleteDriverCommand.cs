using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class DeleteDriverCommand : ICommand
    {
        public DeleteDriverCommand(Guid driverId)
        {
            DriverId = driverId;
        }

        public Guid DriverId { get; set; }
    }
}
