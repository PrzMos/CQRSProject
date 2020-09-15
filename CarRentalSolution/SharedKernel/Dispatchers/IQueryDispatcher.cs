
namespace SharedKernel.Dispatchers
{
    public interface IQueryDispatcher
    {
        TResult Send<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;
    }
}