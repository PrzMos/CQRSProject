using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands.Handlers
{
    public class DeleteCarCommandHandler : CommandHandlerBase, ICommandHandler<DeleteCarCommand>
    {
        public DeleteCarCommandHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public void Execute(DeleteCarCommand command)
        {
            var carToDelete = _unitOfWork.CarRepository.Get(command.Id);
            var carReadModelToDelete = _unitOfWork.CarReadModel.Get(command.Id);

            if (carToDelete == null || carReadModelToDelete == null)
            {
                throw new Exception($"Car with passed id: {command.Id} doesn't exist please check if it is correct.");
            }

            _unitOfWork.CarReadModel.Delete(carReadModelToDelete);
            _unitOfWork.CarRepository.Delete(carToDelete);

            _unitOfWork.Commit();
        }

        public void Execute(DeleteCarCommand command, out Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}
