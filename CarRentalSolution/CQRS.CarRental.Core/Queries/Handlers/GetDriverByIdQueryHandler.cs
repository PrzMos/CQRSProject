using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Results;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public class GetDriverByIdQueryHandler : QueryHandlerBase, IQueryHandler<GetDriverByIdQuery, DriverResult>
    {
        public GetDriverByIdQueryHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public DriverResult Execute(GetDriverByIdQuery query)
        {
            if (query.DriverId != Guid.Empty)
            {
                var driver = _unitOfWork.DriverRepository.Get(query.DriverId);

                return _mapper.Map<DriverResult>(driver);
            }

            throw new Exception("Passed DriverId is empty!");
        }
    }
}
