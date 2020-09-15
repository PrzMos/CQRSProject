using System;

namespace SharedKernel.Persistance
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
        void RejectChanges();
    }
}
