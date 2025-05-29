using Application.Features.DriverFeatures.DTOs;
using AutoMapper;
using Domain.Entities.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Mapping
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>();
        }
    }
}
