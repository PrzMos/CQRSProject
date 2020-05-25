using AutoMapper;
using CQRS.CarRental.Core.Interfaces;
using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CarRental.Core.Commands.Handlers
{
    public class DeleteRentCommandHandler : CommandHandlerBase, ICommandHandler<DeleteRentCommand>
    {
        public DeleteRentCommandHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public void Execute(DeleteRentCommand command)
        {
            var rentToDelete = _unitOfWork.RentalRepository.Get(command.Id);
            var rentReadModelToDelete = _unitOfWork.RentalReadModel.Get(command.Id);

            if (rentToDelete == null||rentReadModelToDelete==null)
            {
                throw new Exception($"Rent within Id:{command.Id} doesn't exist. Verify number and try again.");
            }

            _unitOfWork.RentalRepository.Delete(rentToDelete);
            _unitOfWork.RentalReadModel.Delete(rentReadModelToDelete);

            _unitOfWork.Commit();
        }

        public void Execute(DeleteRentCommand command, out Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}
