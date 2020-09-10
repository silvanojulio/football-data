using AutoMapper;
using FootballDataCommon.Contracts.Entities;
using FootballDataCommon.Contracts.Entities.ApiEntities;
using FootballDataRepository.DbModel;
using System;

namespace FootballDataApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeageApiDto, Competition>()
                .ForMember(x => x.AreaName, options => options.MapFrom(src => src.area.name))
                .ForMember(x => x.Code, options => options.MapFrom(src => src.code))
                .ForMember(x => x.Id, options => options.MapFrom(src => src.id))
                .ForMember(x => x.Name, options => options.MapFrom(src => src.name));

            CreateMap<TeamApiDto, Team>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.id))
                .ForMember(x => x.Email, options => options.MapFrom(src => src.email))
                .ForMember(x => x.ShortName, options => options.MapFrom(src => src.shortName))
                .ForMember(x => x.Name, options => options.MapFrom(src => src.name));

            CreateMap<PlayerApiDto, Player>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.id))
                .ForMember(x => x.Name, options => options.MapFrom(src => src.name))
                .ForMember(x => x.CountryOfBirth, options => options.MapFrom(src => src.countryOfBirth))
                .ForMember(x => x.Position, options => options.MapFrom(src => src.position))
                .ForMember(x => x.DateOfBirth, options => options.MapFrom(src => src.dateOfBirth))
                .ForMember(x => x.Nationality, options => options.MapFrom(src => src.nationality));

        }
    }
}
