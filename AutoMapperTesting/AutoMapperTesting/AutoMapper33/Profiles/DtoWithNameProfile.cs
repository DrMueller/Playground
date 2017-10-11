using AutoMapper;
using Common.Dtos;
using Common.Models;

namespace AutoMapper33.Profiles
{
    public abstract class DtoWithNameProfile<TDto> : Profile
        where TDto : IDtoWithName
    {
        protected override void Configure()
        {
            CreateMap<NameProvider, TDto>()
                .ForMember(d => d.Name, c => c.MapFrom(f => f.SomeName));
        }
    }
}