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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<CreateRentCommand, Rental>().ReverseMap();
            CreateMap<RentalReadModel, RentalResult>()
                .ForMember(opt=>opt.Started,memberOptions=>memberOptions.MapFrom(x=>x.Created))
                .ForMember(opt=>opt.Finished,memberOptions=>memberOptions.MapFrom(x=>x.Finished)).ReverseMap();
            CreateMap<RentalResult, RentalModelResult>()
                .ForMember(opt=>opt.Started, memberOptions=>memberOptions.MapFrom(x=>x.Started))
                .ForMember(opt=>opt.Finished,memberOptions=>memberOptions.MapFrom(x=>x.Finished)).ReverseMap();
            CreateMap<RentalReadModel, RentalModelResult>()
                .ForMember(opt => opt.Started, memberOptions => memberOptions.MapFrom(x => x.Created))
                .ForMember(opt => opt.Finished, memberOptions => memberOptions.MapFrom(x => x.Finished)).ReverseMap();
            CreateMap<Rental, RentalResult>().ReverseMap();
            CreateMap<CreateRentCommand, RentalModelResult>().ReverseMap();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
