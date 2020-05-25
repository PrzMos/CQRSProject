using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class StopRentingCarCommand : ICommand
    {
        public Guid RentId { get; set; }
        public DateTime Finished { get; set; }
        public double StopX { get; set; }
        public double StopY { get; set; }
    }
}
