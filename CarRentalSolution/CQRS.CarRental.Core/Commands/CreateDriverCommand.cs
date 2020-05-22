using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class CreateDriverCommand : ICommand
    {
        public string LicenceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
