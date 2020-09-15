using AutoMapper;
using CQRS.CarRental.Core.Commands;
using CQRS.CarRental.Core.Models.Read;
using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CQRS.CarRental.RESTAPI.Profiles
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<CreateDriverCommand, Driver>().ReverseMap();
            CreateMap<Driver, DriverViewModel>().ReverseMap();
            CreateMap<Driver, DriverResult>()
                .ForMember(opt=>opt.FullName,
                            opt=>opt.MapFrom(src=>src.GetFullName()));
            CreateMap<CreateDriverCommand, DriverResult>()
                .ForMember(opt => opt.FullName,
                            opt => opt.MapFrom(src => src.GetFullName()));
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
