using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapperLatest.Profiles;
using Common.Dtos;
using Common.Models;

namespace AutoMapperLatest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("In Latest!");
            Console.WriteLine();

            var mapperConfig = new MapperConfiguration(f =>
                                                       {
                                                           f.AddProfiles(typeof(Program));
                                                       });

            var mapper = mapperConfig.CreateMapper();

            var nameProvider = new NameProvider();
            nameProvider.SomeName = "Test Individual Name";
            nameProvider.SomeOtherName = "Some Other Name";

            var dtoFromIndividual = mapper.Map<IndividualDto>(nameProvider);
            Console.WriteLine(dtoFromIndividual.Name);
            Console.WriteLine(dtoFromIndividual.Date);
            Console.WriteLine(dtoFromIndividual.Name2);

            nameProvider.SomeName = "Test Organisation Name";
            var dtoFromOrganisation = mapper.Map<OrganisationDto>(nameProvider);
            Console.WriteLine(dtoFromOrganisation.Name);
            Console.WriteLine(dtoFromOrganisation.Date);

            Console.ReadKey();

        }
    }
}
