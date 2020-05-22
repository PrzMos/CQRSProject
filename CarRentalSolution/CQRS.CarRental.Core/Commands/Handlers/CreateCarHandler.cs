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
    public class CreateCarHandler : CommandHandlerBase, ICommandHandler<CreateCarCommand>
    {

        public CreateCarHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper)
        {
        }

        public void Execute(CreateCarCommand command)
        {
            var carToCreate = _mapper.Map<Car>(command);
            carToCreate.CarId = Guid.NewGuid();

            _unitOfWork.CarRepository.Insert(carToCreate);

            var carReadModel = _mapper.Map<CarViewModel>(carToCreate);

            _unitOfWork.CarReadModel.Insert(carReadModel);

            _unitOfWork.Commit();
        }
    }
}
