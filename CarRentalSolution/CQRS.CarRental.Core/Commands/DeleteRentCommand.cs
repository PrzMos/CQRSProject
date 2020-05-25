using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class DeleteRentCommand : ICommand
    {
        public Guid Id { get; set; }
        public DeleteRentCommand(Guid id)
        {
            Id = id;
        }
    }
}
