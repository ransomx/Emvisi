namespace Emvisi.MigrationsBI
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Emvisi.Models.BIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"MigrationsBI";
            ContextKey = "Emvisi.Models.BIContext";
        }

        protected override void Seed(Emvisi.Models.BIContext context)
        {
            var regions = new List<Region>
            {
                new Region { Name = "Smaland", Cities = new List<City>() },
                new Region { Name = "Gotland", Cities = new List<City>() },
                new Region { Name = "Blekinge", Cities = new List<City>() },
                new Region { Name = "Uppland", Cities = new List<City>() },
                new Region { Name = "Scania", Cities = new List<City>() }
            };
            regions.ForEach(s => context.Regions.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var cities = new List<City>
            {
                new City { Name = "Hassleholm", Region = regions.Single( i => i.Name == "Scania") },
                new City { Name = "Solvesborg", Region = regions.Single( i => i.Name == "Scania") },
                new City { Name = "Malmo", Region = regions.Single( i => i.Name == "Scania") },
                new City { Name = "Kristianstad", Region = regions.Single( i => i.Name == "Scania")}
            };
            cities.ForEach(s => context.Cities.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            Address address = new Address { City = cities.Single(i => i.Name == "Kristianstad"), Street = "Nyvagen 410", Zip = "45 200" };
            Address address2 = new Address { City = cities.Single(i => i.Name == "Kristianstad"), Street = "Kolonivagen 50", Zip = "45200" };
            context.Addresses.AddOrUpdate(address);
            context.Addresses.AddOrUpdate(address2);
            context.SaveChanges();

            ContactInfo info1 = new ContactInfo { Address = address, Email = "gymadmin@bros.com", Phone = "+420773456987" };
            ContactInfo info2 = new ContactInfo { Address = address2, Email = "gymadmin2@bros.com", Phone = "+420773456987" } ;
            context.ContactInfos.AddOrUpdate(info1);
            context.ContactInfos.AddOrUpdate(info2);
            Position position1 = new Position { GeoLat = "56.02939360000001", GeoLong = "14.156677800000011"};
            Position position2 = new Position { GeoLat = "56.022948", GeoLong = "14.133911" };
            context.Positions.AddOrUpdate(position1);
            context.Positions.AddOrUpdate(position2);
            context.SaveChanges();

            Activity activity = new Activity { Title = "Bodybuilding", Icon = "dumbbell", Gyms = new List<Gym>() };
            Activity swimming = new Activity { Title = "Swimming", Icon = "swimming", Gyms = new List<Gym>() };
            Facility facility = new Facility { Title = "WiFi", Icon = "wifi", Gyms = new List<Gym>() };

            Gym gym = new Models.Gym {
                Title = "Fitness24Seven",
                BrandName = "Fitness24Seven",
                LogoFilePath = "fitness24seven/logo/fitness24seven.jpg",
                Description = "A gym located in Kristianstad, quite regular and well maintained",
                ContactInfo = info1,
                Position = position1
            };

            Gym gym2 = new Models.Gym
            {
                Title = "Fitness24Seven2",
                BrandName = "Fitness24Seven",
                Description = "Another gym of f24s, located in Kristianstad, quite regular and well maintained as usual",
                LogoFilePath = "fitness24seven2/logo/fitness24seven2.jpg",
                ContactInfo = info2,
                Position = position2
            };
            gym.Images.Add(new Image { Path = "fitness24seven/imgs/f24s_0.jpg" });
            context.Gyms.AddOrUpdate(gym);
            context.Gyms.AddOrUpdate(gym2);
            Subscription subscription = new Subscription { Title = "Regular", Price = "2400", Gym = gym};
            gym.HasActivities.Add(activity);
            gym.HasFacilities.Add(facility);
            gym2.HasActivities.Add(swimming);
            gym.HasSubscriptions.Add(subscription);
            context.SaveChanges();
        }
    }
}
