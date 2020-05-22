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
    public class CreateRentHandler : CommandHandlerBase, ICommandHandler<CreateRentCommand>
    {
        public CreateRentHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper)
        {
        }

        public void Execute(CreateRentCommand command)
        {
            var rentToCreate = _mapper.Map<Rental>(command);

            rentToCreate.RentalId = Guid.NewGuid();

            var carToRent = _unitOfWork.CarRepository.Get(command.CarId);
            carToRent.Status = Status.wypożyczony;

            var driver = _unitOfWork.DriverRepository.Get(command.DriverId);

            _unitOfWork.RentalRepository.Insert(rentToCreate);

            var rentalReadModel = new RentalReadModel()
            {
                CarId = command.CarId,
                Driver = driver.GetFullName(),
                DriverId = driver.DriverId,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                RegistrationNumber = carToRent.RegistrationNumber,
                RentalId = rentToCreate.RentalId,
                StartXPosition = carToRent.XPosition,
                StartYPosition = carToRent.YPosition,
                Total = 0
            };
        }
    }
}
