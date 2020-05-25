using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Results;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public class GetAllDriversQueryHandler : QueryHandlerBase, IQueryHandler<GetAllDriversQuery, List<DriverResult>>
    {
        public GetAllDriversQueryHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public List<DriverResult> Execute(GetAllDriversQuery query)
        {
            var drivers = _unitOfWork.DriverRepository.GetAll();

            var driversToReturn = _mapper.Map<List<DriverResult>>(drivers);

            return driversToReturn;
        }
    }
}
