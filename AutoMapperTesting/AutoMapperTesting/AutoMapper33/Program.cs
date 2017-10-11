using System;
using AutoMapper;
using AutoMapper33.Profiles;
using Common.Dtos;
using Common.Models;

namespace AutoMapper33
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("In Writeline!");
            Console.WriteLine();

            Mapper.AddProfile(new OrganisationDtoProfile());
            Mapper.AddProfile(new IndividualDtoProfile());

            var nameProvider = new NameProvider();
            nameProvider.SomeName = "Test Individual Name";

            var dtoFromIndividual = Mapper.Map<IndividualDto>(nameProvider);
            Console.WriteLine(dtoFromIndividual.Name);

            nameProvider.SomeName = "Test Organisation Name";
            var dtoFromOrganisation = Mapper.Map<OrganisationDto>(nameProvider);
            Console.WriteLine(dtoFromOrganisation.Name);

            Console.ReadKey();
        }
    }
}