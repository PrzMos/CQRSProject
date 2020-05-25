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
    public class StopRentingCarHandler : CommandHandlerBase, ICommandHandler<StopRentingCarCommand>
    {
        public StopRentingCarHandler(ICarRentalUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public void Execute(StopRentingCarCommand command)
        {
            var rent = _unitOfWork.RentalRepository.GetRentalWithDriverAndCarDetails(command.RentId);

            Car car = _unitOfWork.CarRepository.Get(rent.CarId);
            car.UpdatePositions(command.StopX, command.StopY);
            car.ChangeStatus();

            rent.Finished = command.Finished;
            rent.Total = rent.GetTotalPrice();

            RentalReadModel rental = _unitOfWork.RentalReadModel.Get(command.RentId);

            if (rental !=null)
            {
                rental.Finished = command.Finished;
                rental.Total = rent.Total;
                rental.StopXPosition = command.StopX;
                rental.StopYPosition = command.StopY;
            }

            var carReadModel = _unitOfWork.CarReadModel.Get(rent.CarId);
            carReadModel.UpdatePositions(command.StopX, command.StopY);
            carReadModel.ChangeStatus();

            _unitOfWork.Commit();
        }

        public void Execute(StopRentingCarCommand command, out Guid itemId)
        {
            itemId = command.RentId;
            Execute(command);
        }
    }
}
