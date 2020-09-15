using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Dispatchers
{
    public interface IQueryHandler
    { }


    public interface IQueryHandler<TQuery, TResult> : IQueryHandler
        where TQuery : IQuery
    {
        TResult Execute(TQuery query);
    }
}
