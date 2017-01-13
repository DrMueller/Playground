using MMU.WpfFunctionalities.TestWpfUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMU.WpfFunctionalities.TestWpfUI.Factories
{
    internal static class PersonFactory
    {
        internal static IEnumerable<Person> CreatePersons()
        {
            var lst = new List<Person>();

            lst.Add(new Person
            {
                BirthDate = new DateTime(1985, 03, 02),
                FirstName = "Stefanie",
                HasAddress = false,
                Height = 160,
                LastName = "Heinzmann"
            });

            lst.Add(new Person
            {
                BirthDate = new DateTime(1986, 12, 29),
                FirstName = "Matthias",
                LastName = "Müller",
                Height = 185,
                HasAddress = true
            });

            lst.Add(new Person
            {
                BirthDate = new DateTime(1990, 03, 06),
                FirstName = "Justin",
                LastName = "Bieber",
                Height = 175,
                HasAddress = true
            });

            return lst;
        }
    }
}
