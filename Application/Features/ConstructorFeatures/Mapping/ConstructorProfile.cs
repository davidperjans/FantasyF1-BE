using Application.Features.ConstructorFeatures.DTOs;
using AutoMapper;
using Domain.Entities.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ConstructorFeatures.Mapping
{
    public class ConstructorProfile : Profile
    {
        public ConstructorProfile()
        {
            CreateMap<Constructor, ConstructorDto>();
        }
    }
}
