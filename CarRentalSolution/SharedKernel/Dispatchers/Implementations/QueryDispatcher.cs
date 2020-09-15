using SharedKernel.DIContainers;

namespace SharedKernel.Dispatchers.Implementations
{
    public class QueryDispatcher : IQueryDispatcher
    {
        protected IResolver _resolver;

        public QueryDispatcher(IResolver resolver)
        {
            this._resolver = resolver;
        }

        public TResult Send<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
        {
            IQueryHandler<TQuery, TResult> handler = this._resolver.Resolve<IQueryHandler<TQuery, TResult>>();
            return handler.Execute(query);
        }
    }
}
