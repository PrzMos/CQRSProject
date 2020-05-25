﻿using AutoMapper;
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
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CreateCarCommand>().ReverseMap();
            CreateMap<Car, CarViewModel>().ReverseMap();
            CreateMap<Car, CarResult>();
            CreateMap<CreateCarCommand, CarResult>();
        }
    }
}
