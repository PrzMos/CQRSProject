using System;

namespace SharedKernel.Dispatchers
{
    public interface ICommandDispatcher
    {
        void Send<TCommand>(TCommand command)
               where TCommand : ICommand;
        void Send<TCommand>(TCommand command, out Guid itemId)
            where TCommand : ICommand;
    }
}