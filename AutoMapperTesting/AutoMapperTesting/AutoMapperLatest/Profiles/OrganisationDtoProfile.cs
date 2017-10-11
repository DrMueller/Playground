using Common.Dtos;

namespace AutoMapperLatest.Profiles
{
    public class OrganisationDtoProfile : DtoWithNameProfile<OrganisationDto>
    {
        public OrganisationDtoProfile()
        {
            CreateCoreMap();
        }
    }
}