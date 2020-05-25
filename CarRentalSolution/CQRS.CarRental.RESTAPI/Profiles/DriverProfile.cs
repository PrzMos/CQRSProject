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
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<CreateDriverCommand, Driver>().ReverseMap();
            CreateMap<CreateDriverCommand, DriverResult>().ReverseMap();
            CreateMap<Driver, DriverViewModel>().ReverseMap();
            CreateMap<Driver, DriverResult>()
                .ForMember(opt=>opt.FullName,
                            opt=>opt.MapFrom(src=>src.GetFullName()));
        }
    }
}
