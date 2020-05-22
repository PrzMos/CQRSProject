using CQRS.CarRental.Core.Models.Write;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class CreateCarCommand : ICommand
    {
        public string RegistrationNumber { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double CurrentDistance { get; set; }
        public double TotalDistance { get; set; }
        public StatusCommand Status { get; set; }
    }

    public enum StatusCommand
    {
        wolny, wypożyczony
    }
}
