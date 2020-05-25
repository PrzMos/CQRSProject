using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using CQRS.CarRental.Core.Models.Write;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands.Handlers
{
    public class CreateDriverHandler : CommandHandlerBase, ICommandHandler<CreateDriverCommand>
    {

        public CreateDriverHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public void Execute(CreateDriverCommand command)
        {
            var driverToCreate = _mapper.Map<Driver>(command);

            driverToCreate.DriverId = Guid.NewGuid();

            Run(driverToCreate);
        }

        public void Execute(CreateDriverCommand command, out Guid itemId)
        {
            var driverToCreate = _mapper.Map<Driver>(command);
            
            driverToCreate.DriverId = Guid.NewGuid();

            itemId = driverToCreate.DriverId;

            Run(driverToCreate);
        }

        private void Run(Driver driverToCreate)
        {
            _unitOfWork.DriverRepository.Insert(driverToCreate);

            var driverReadModel = _mapper.Map<DriverViewModel>(driverToCreate);

            _unitOfWork.DriverRead.Insert(driverReadModel);

            _unitOfWork.Commit();
        }
    }
}
