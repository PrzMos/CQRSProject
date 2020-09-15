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
    public class CreateCarCommandHandler : CommandHandlerBase, ICommandHandler<CreateCarCommand>
    {

        public CreateCarCommandHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper)
        {
        }

        public void Execute(CreateCarCommand command)
        {
            var carToCreate = _mapper.Map<Car>(command);
            
            carToCreate.CarId = Guid.NewGuid();

            Run(carToCreate);
        }

        public void Execute(CreateCarCommand command, out Guid itemId)
        {
            var carToCreate = _mapper.Map<Car>(command);
            
            carToCreate.CarId = Guid.NewGuid();

            itemId = carToCreate.CarId;

            Run(carToCreate);
        }

        private void Run(Car car)
        {
            _unitOfWork.CarRepository.Insert(car);

            var carReadModel = _mapper.Map<CarViewModel>(car);

            _unitOfWork.CarReadModel.Insert(carReadModel);

            _unitOfWork.Commit();
        }
    }
}
