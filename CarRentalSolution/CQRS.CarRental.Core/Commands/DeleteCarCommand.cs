using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    public class DeleteCarCommand : ICommand
    {
        public DeleteCarCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
