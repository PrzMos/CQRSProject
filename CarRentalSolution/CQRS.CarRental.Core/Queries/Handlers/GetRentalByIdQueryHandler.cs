using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Results;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public class GetRentalByIdQueryHandler : QueryHandlerBase, IQueryHandler<GetRentalByIdQuery, RentalModelResult>
    {
        public GetRentalByIdQueryHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public RentalModelResult Execute(GetRentalByIdQuery query)
        {
            if (query.RentalId != Guid.Empty)
            {
                var rental = _unitOfWork.RentalReadModel.Get(query.RentalId);

                var rentalToReturn = _mapper.Map<RentalModelResult>(rental);

                return rentalToReturn;
            }

            throw new Exception("Given Rental Id was Empty!");
        }
    }
}
