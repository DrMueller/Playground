using System;

namespace Common.Dtos
{
    public interface IDtoWithName
    {
        DateTime Date { get; set; }
        string Name { get; set; }
    }
}