using Common.Dtos;
using Common.Models;

namespace AutoMapperLatest.Profiles
{
    public class IndividualDtoProfile : DtoWithNameProfile<IndividualDto>
    {
        public IndividualDtoProfile()
        {
            CreateCoreMap()
                .ForMember(d => d.Name2, c => c.MapFrom(f => f.SomeOtherName));
        }
    }
}