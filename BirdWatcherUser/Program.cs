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
            EndangeredBirds();
            SightingsOfEndangeredBirds();
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

        static int EndangeredBirds()
        {
           

            var count = birds
              .Where(b => b.ConservationStatus == "Endangered")
              .Select(b => b.Sightings)
              .Count();

            Console.WriteLine("We have sightings of {0} endangered birds", count);
            return count;
        }

      

        static int SightingsOfEndangeredBirds()
        {
            var statuses = birds
                .Select(b => b.ConservationStatus)
                .Distinct()
                .Where(s => s != "LeastConcern" && s != "NearThreatened");

            var sightings = birds.SelectMany(b => b.Sightings);


            var endangeredSightings = sightings.Join
            (
                statuses,
                sighting => sighting.Bird.ConservationStatus,
                status => status,
                (s, st) => new { Sighting = s, Status = st }
            )
            .GroupBy(n => n.Status)
            .Select(g => new { Status = g.Key, SightingsCount = g.Count() }); // I think the point here
            // is that now we can only use Key or aggregate functions

            foreach(var s in endangeredSightings)
            {
                Console.WriteLine(s.Status + " " + s.SightingsCount);
            }

            var count = endangeredSightings.Sum(e => e.SightingsCount);

            Console.WriteLine("We have {0} sightings of endangered birds", count);
            return 0;
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
