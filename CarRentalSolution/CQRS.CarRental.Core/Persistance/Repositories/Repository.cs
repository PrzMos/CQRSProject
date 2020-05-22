using SharedKernel.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CQRS.CarRental.Core.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CarRentalContext _carRentalContext;

        public Repository(CarRentalContext carRentalContext)
        {
            _carRentalContext = carRentalContext ?? throw new ArgumentNullException(nameof(carRentalContext));
        }

        public void Delete(TEntity entity)
        {
            _carRentalContext.Set<TEntity>().Remove(entity);
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return  _carRentalContext.Set<TEntity>().Where(expression).ToList();
        }

        public TEntity Get(Guid id)
        {
            return _carRentalContext.Set<TEntity>().Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return _carRentalContext.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            _carRentalContext.Set<TEntity>().Add(entity);
        }
    }
}
