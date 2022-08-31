using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Viagogo
{
    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Solution
    {
        static void Main(string[] args)
        {
            var events = new List<Event>{
new Event{ Name = "Phantom of the Opera", City = "New York"},
new Event{ Name = "Metallica", City = "Los Angeles"},
new Event{ Name = "Metallica", City = "New York"},
new Event{ Name = "Metallica", City = "Boston"},
new Event{ Name = "LadyGaGa", City = "New York"},
new Event{ Name = "LadyGaGa", City = "Boston"},
new Event{ Name = "LadyGaGa", City = "Chicago"},
new Event{ Name = "LadyGaGa", City = "San Francisco"},
new Event{ Name = "LadyGaGa", City = "Washington"}
};
            
            var customer = new Customer { Name = "Mr. Fake", City = "New York" };
            //1.1 finding the same city
            var sameCityEvents = events.Where(x => x.City == customer.City);
            //1.2 Sending the email
            
            foreach (var item in sameCityEvents)
            {
                AddToEmail(customer, item);
            }

            //1.3 Only emails will be send to one client John Smith
            //1.4 It can be improved in some ways
            //a)Pick an event and find all customers in the same city and send them the email at once.
            //This way by looping your events all you customers will be served.
            //b) You can loop the customers if count of customer is not too much. Find all the events in the customer city and
            //send that all the events in one email, but this would require changing the AddtoEmail function.


            //2.
            AddToEmail(customer, events);
            //3 Check the statuscode of the API call, if its other than 200 then dont process the event.
            //4 Save failed events in a queue or a table. Saved records can be processed again and can't be used by developer to figure out failure.
            //5 We can use linq OrderBy on price to get it sorted and We can also use ThenBy to sort or multiple columns.
        }
        //Overloaded the method can calucated Distance by using dictionary.
        public static void AddToEmail(Customer c, List<Event> events)
        {
            var interestedEvents = new List<Event>(5);
            var minimumDistance = new Dictionary<int, double>();
            for (int i = 0; i < events.Count(); i++)
            {
                var distance = GetDistance(c.City,events[i].City);
                if (i < 5)
                {
                    interestedEvents.Add(events[i]);
                    minimumDistance.Add(i, distance);
                }
                else
                {
                    var index = minimumDistance.Where(x => x.Value > distance).OrderByDescending(x => x.Value).FirstOrDefault();
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
                for (int i = 0; i < interestedEvents.LongCount(); i++)
                    Console.Out.WriteLine($"{c.Name}: {interestedEvents[i].Name} in {interestedEvents[i].City}"
                    + (minimumDistance[i] > 0 ? $" ({minimumDistance[i]} miles away)" : ""));
            }

        }

        // You do not need to know how these methods work
        static void AddToEmail(Customer c, Event e, int? price = null)
        {
            var distance = GetDistance(c.City, e.City);
            Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
            + (distance > 0 ? $" ({distance} miles away)" : "")
            + (price.HasValue ? $" for ${price}" : ""));
        }
        static int GetPrice(Event e)
        {
            return (AlphebiticalDistance(e.City, "") + AlphebiticalDistance(e.Name, "")) / 10;
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
                result += Math.Abs(s[i] - t[i]);
            }
            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                result += s.Length > t.Length ? s[i] : t[i];
            }
            return result;
        }
    }
}
/*
var customers = new List<Customer>{
new Customer{ Name = "Nathan", City = "New York"},
new Customer{ Name = "Bob", City = "Boston"},
new Customer{ Name = "Cindy", City = "Chicago"},
new Customer{ Name = "Lisa", City = "Los Angeles"}
};
*/