using AutoMapper;
using CQRS.CarRental.Core.Commands;
using CQRS.CarRental.Core.Models.Read;
using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.CarRental.RESTAPI.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<CreateRentCommand, Rental>();
            CreateMap<RentalReadModel, RentalResult>();
            CreateMap<RentalReadModel, RentalModelResult>();
        }
    }
}
