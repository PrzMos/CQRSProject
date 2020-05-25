using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Results;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public class GetAllCarsQueryHandler :QueryHandlerBase, IQueryHandler<GetAllCarsQuery, List<CarResult>>
    {
        public GetAllCarsQueryHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public List<CarResult> Execute(GetAllCarsQuery query)
        {
            var cars = _unitOfWork.CarRepository.GetAll();

            var carsToReturn = _mapper.Map<List<CarResult>>(cars);

            return carsToReturn;
        }
    }
}
