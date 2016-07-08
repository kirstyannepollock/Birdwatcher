using BirdWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdWatcherUser
{
    class Program
    {
        static List<Bird> birds = BirdRepository.LoadBirds();

        static void Main(string[] args)
        {
            Birdcount();
            SightingsCount();
            SightingsAvg();
            CountryCount();
            ListCountrySightingCounts();
            Console.ReadLine();
        }

        static int  Birdcount()
        {
            var birdCount = birds.Count();
            Console.WriteLine("We have total of {0} birds", birdCount);
            return birdCount;
        }


        static int SightingsCount()
        {
            var countUsingSelectMany = birds.SelectMany(b => b.Sightings).Count();
            var count = birds.Select(b => b.Sightings.Count()).Sum();
            Console.WriteLine("We have total of {0} sightings", count);
           // Console.WriteLine("We have total of {0} sightings (SelectMany)", countUsingSelectMany);
            return count;
        }

        static int SightingsAvg()
        {
            var count = (int)Math.Floor(birds.Select(b => b.Sightings.Count()).Average());
            Console.WriteLine("We have avg of {0} sightings", count);
            return count;
        }

        static void ListCountries()
        {
            // this is exactly the case for the SelectMany "Flattening" method!
            var countries = birds
                .SelectMany
                (
                    b => b.Sightings
                    .Select(s => s.Place.Country)
                )
                .Distinct();

            foreach(var c in countries)
            {
                Console.WriteLine(c);
            };
            
        }

        static void ListCountrySightingCounts()
        {
            // this is exactly the case for the SelectMany "Flattening" method!
            var countries = birds
                .SelectMany(b => b.Sightings)
                .GroupBy(s => s.Place.Country)
                .Select(g => new { Country = g.Key, Count = g.Count() });

            foreach (var c in countries)
            {
                Console.WriteLine("{0},{1}", c.Country, c.Count);
            };

        }

        static int CountryCount()
        {
            // this is exactly the case for the SelectMany "Flattening" method!
            var count = birds
                .SelectMany
                (
                    b => b.Sightings
                    .Select(s => s.Place.Country)
                )
                .Distinct()
                .Count();

            Console.WriteLine("We have sightings in {0} countries", count);
            return count;
        }
    }
}
