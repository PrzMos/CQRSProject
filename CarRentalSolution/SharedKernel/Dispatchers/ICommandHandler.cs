using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Dispatchers
{
    public interface ICommandHandler
    { }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void Execute(TCommand command);
        void Execute(TCommand command, out Guid itemId);
    }
}
