using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands.Handlers
{
    public class CommandHandlerBase
    {
        protected readonly ICarRentalUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public CommandHandlerBase(ICarRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
