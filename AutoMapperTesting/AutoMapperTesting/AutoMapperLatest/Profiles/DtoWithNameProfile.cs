using System;
using AutoMapper;
using Common.Dtos;
using Common.Models;

namespace AutoMapperLatest.Profiles
{
    public abstract class DtoWithNameProfile<TDto> : Profile
        where TDto : IDtoWithName
    {
        protected IMappingExpression<NameProvider, TDto> CreateCoreMap()
        {
            return CreateMap<NameProvider, TDto>()
                .ForMember(d => d.Name, c => c.MapFrom(f => f.SomeName))
                .ForMember(d => d.Date, c => c.UseValue(DateTime.Now));
        }
    }
}