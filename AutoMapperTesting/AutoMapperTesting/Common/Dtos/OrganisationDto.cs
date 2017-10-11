using System;

namespace Common.Dtos
{
    public class OrganisationDto : IDtoWithName
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string SomeOtherName { get; set; }
    }
}