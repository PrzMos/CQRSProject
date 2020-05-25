using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Models.Read;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands.Handlers
{
    public class DeleteDriverCommandHandler : CommandHandlerBase, ICommandHandler<DeleteDriverCommand>
    {
        public DeleteDriverCommandHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public void Execute(DeleteDriverCommand command)
        {
            var driverToDelete = _unitOfWork.DriverRepository.Get(command.DriverId);

            if (driverToDelete == null)
            {
                return;
            }

            _unitOfWork.DriverRepository.Delete(driverToDelete);

            var driverReadModel = _mapper.Map<DriverViewModel>(driverToDelete);

             _unitOfWork.DriverRead.Delete(driverReadModel);

            _unitOfWork.Commit();
        }

        public void Execute(DeleteDriverCommand command, out Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}
