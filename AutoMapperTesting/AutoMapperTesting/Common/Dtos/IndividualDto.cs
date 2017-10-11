using System;

namespace Common.Dtos
{
    public class IndividualDto : IDtoWithName
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
    }
}
