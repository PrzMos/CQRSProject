using SharedKernel.DIContainers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Dispatchers.Implementations
{
    public class CommandDispatcher : ICommandDispatcher
    {
        protected IResolver _resolver;
        
        public CommandDispatcher(IResolver resolver)
        {
            this._resolver = resolver;
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = this._resolver.Resolve<ICommandHandler<TCommand>>();
            handler.Execute(command);
        }

        public void Send<TCommand>(TCommand command, out Guid itemId) where TCommand : ICommand
        {
            var handler = _resolver.Resolve<ICommandHandler<TCommand>>();
            handler.Execute(command, out itemId);

        }
    }
}
