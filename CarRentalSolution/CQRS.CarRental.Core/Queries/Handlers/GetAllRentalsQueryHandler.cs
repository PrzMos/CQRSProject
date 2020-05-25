using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Results;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public class GetAllRentalsQueryHandler : QueryHandlerBase, IQueryHandler<GetAllRentalsQuery, List<RentalResult>>
    {
        public GetAllRentalsQueryHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public List<RentalResult> Execute(GetAllRentalsQuery query)
        {
            var rentals = _unitOfWork.RentalReadModel.GetAll();

            var rentalsToReturn = _mapper.Map<List<RentalResult>>(rentals);

            return rentalsToReturn;
        }
    }
}
