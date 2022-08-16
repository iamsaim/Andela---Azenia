using AndelaDummyTest.Entities;
using AndelaDummyTest.Service;

var cities = new List<City> {
new City { Id = 1, Name = "New York", Coordinates = new Coordinates{ Longitude = 5, Latitude = 5} },
new City { Id = 2, Name = "Los Angeles", Coordinates = new Coordinates{ Longitude = 10, Latitude = 2} },
new City { Id = 3, Name = "Boston", Coordinates = new Coordinates{ Longitude = 12, Latitude = 4} },
new City { Id = 4, Name = "Chicago", Coordinates = new Coordinates{ Longitude = 8, Latitude = 7} },
new City { Id = 5, Name = "San Francisco", Coordinates = new Coordinates{ Longitude = 1, Latitude = 1} },
new City { Id = 6, Name = "Washington", Coordinates = new Coordinates{ Longitude = 3, Latitude = 12} },
};
var events = new List<Event>{
new Event{ Name = "Phantom of the Opera", CityId = cities.FirstOrDefault(x=>x.Name == "New York" ).Id},
new Event{ Name = "Metallica", CityId = cities.FirstOrDefault(x=>x.Name == "Los Angeles" ).Id},
new Event{ Name = "Metallica", CityId = cities.FirstOrDefault(x=>x.Name == "New York" ).Id},
new Event{ Name = "Metallica", CityId = cities.FirstOrDefault(x=>x.Name == "Boston" ).Id},
new Event{ Name = "LadyGaGa", CityId = cities.FirstOrDefault(x=>x.Name == "New York" ).Id},
new Event{ Name = "LadyGaGa", CityId = cities.FirstOrDefault(x=>x.Name == "Boston" ).Id},
new Event{ Name = "LadyGaGa", CityId = cities.FirstOrDefault(x=>x.Name == "Chicago" ).Id},
new Event{ Name = "LadyGaGa", CityId = cities.FirstOrDefault(x=>x.Name == "San Francisco" ).Id},
new Event{ Name = "LadyGaGa", CityId = cities.FirstOrDefault(x=>x.Name == "Washington" ).Id}
};

var customer = new Customer { Name = "Mr. Fake", CityId = cities.FirstOrDefault(x => x.Name == "New York").Id };
//1.1 Get all events in the city of the customer
var nearByEvents = events.Where(e => e.CityId == customer.CityId);
// 1.2 Send all events to a single email TASK

//1.3 If no events then no email
if(nearByEvents.Count()>0)
    EventManagementService.AddEventsToEmail(customer, nearByEvents,cities.FirstOrDefault(x=>x.Id == customer.CityId).Name);

//2
EventManagementService.AddToEmail(customer,events,cities);
/*
* We want you to send an email to this customer with all events in their city
* Just call AddToEmail(customer, event) for each event you think they should get
*/