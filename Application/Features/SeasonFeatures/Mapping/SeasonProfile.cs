using Application.Features.SeasonFeatures.DTOs;
using AutoMapper;
using Domain.Entities.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.Mapping
{
    public class SeasonProfile : Profile
    {
        public SeasonProfile()
        {
            CreateMap<Season, SeasonDto>();
        }
    }
}
