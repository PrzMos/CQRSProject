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

            Run(rentToCreate);
        }

        public void Execute(CreateRentCommand command, out Guid itemId)
        {
            var rentToCreate = _mapper.Map<Rental>(command);

            rentToCreate.RentalId = Guid.NewGuid();

            itemId = rentToCreate.RentalId;

            Run(rentToCreate);
        }

        private void Run(Rental rentToCreate)
        {
            var carToRent = _unitOfWork.CarRepository.Get(rentToCreate.CarId);
            carToRent.Status = Status.wypożyczony;

            var driver = _unitOfWork.DriverRepository.Get(rentToCreate.DriverId);

            _unitOfWork.RentalRepository.Insert(rentToCreate);

            var rentalReadModel = new RentalReadModel()
            {
                CarId = rentToCreate.CarId,
                Driver = driver.GetFullName(),
                DriverId = driver.DriverId,
                Created = DateTime.Now,
                RegistrationNumber = carToRent.RegistrationNumber,
                RentalId = rentToCreate.RentalId,
                StartXPosition = carToRent.XPosition,
                StartYPosition = carToRent.YPosition,
                Total = 0
            };

            _unitOfWork.RentalReadModel.Insert(rentalReadModel);

            var carReadModel = _unitOfWork.CarReadModel.Get(carToRent.CarId);
            carReadModel.Status = Status.wypożyczony;

            _unitOfWork.Commit();
        }
    }
}
