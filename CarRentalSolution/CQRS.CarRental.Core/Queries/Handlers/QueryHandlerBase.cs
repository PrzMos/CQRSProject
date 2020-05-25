using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Queries.Handlers
{
    public abstract class QueryHandlerBase
    {
        protected readonly ICarRentalUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected QueryHandlerBase(ICarRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
