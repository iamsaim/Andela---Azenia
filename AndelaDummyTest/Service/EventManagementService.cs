using AndelaDummyTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndelaDummyTest.Service
{
    public class EventManagementService
    {
        public static void AddToEmail(Customer c, List<Event> events,List<City> cities)
        {
            var interestedEvents  = new List<Event>(5);
            var minimumDistance = new Dictionary<int, double>();
            var city = cities.FirstOrDefault(x=> x.Id == c.CityId);
            for(int i = 0;i<events.Count();i++)
            {
                var EventCity = cities.FirstOrDefault(x=>x.Id == events[i].CityId);
                var distance = city.Coordinates.DistanceTo(EventCity.Coordinates);
                if (i < 5)
                {
                    interestedEvents.Add(events[i]);
                    minimumDistance.Add(i, distance);
                }
                else
                {
                    var index = minimumDistance.Where(x => x.Value > distance).OrderByDescending(x=>x.Value).FirstOrDefault();
                    if (index.Value > 0)
                    {
                        interestedEvents[index.Key] = events[i];
                        minimumDistance[index.Key] = distance;
                    }
                    
                }


            }

            if (interestedEvents.Count() > 0)
            {
                Console.Out.WriteLine($"{c.Name} following are the events near you : ");
                foreach (var e in interestedEvents)
                    Console.Out.WriteLine($"{e.Name} in {cities.FirstOrDefault(x => x.Id == e.CityId).Name}");
            }
            
        }

        public static void AddEventsToEmail(Customer c, IEnumerable<Event> events,string City)
        {
            Console.Out.WriteLine($"{c.Name} following are the events in {City}: ");
            foreach (var e in events)
                Console.Out.WriteLine($"{e.Name}");
            
        }
       
        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }
        private static int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                result += Math.Abs(s[i] - t[i]);
            }
            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                result += s.Length > t.Length ? s[i] : t[i];
            }
            return result;
        }

    }
}
