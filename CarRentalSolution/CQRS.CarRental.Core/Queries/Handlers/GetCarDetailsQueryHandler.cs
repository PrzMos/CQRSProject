using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Results;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public class GetCarDetailsQueryHandler : QueryHandlerBase, IQueryHandler<GetCarDetailsQuery, CarResult>
    {
        public GetCarDetailsQueryHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public CarResult Execute(GetCarDetailsQuery query)
        {
            if (query.CarId != Guid.Empty)
            {
                var car = _unitOfWork.CarRepository.Get(query.CarId);

                var carToReturn = _mapper.Map<CarResult>(car);

                return carToReturn;
            }

            throw new Exception("Value carId is empty.");
        }
    }
}
