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
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationsBI";
            ContextKey = "Emvisi.Models.BIContext";
        }

        protected override void Seed(Emvisi.Models.BIContext context)
        {
            Address address = new Address {City = "Kristianstad", Street = "Nyvagen 410", Zip = "45 200" };
            Activity activity = new Models.Activity { Title = "Bodybuilding", Icon = "Bodybuilder.png", Gyms = new List<Gym>() };
            Facility facility = new Facility { Title = "WiFi", Icon = "Wifi.png", Gyms = new List<Gym>() };
            Gym gym = new Models.Gym { Title = "Fitness24Seven", Brand = "Fitness24Seven", Phone = "+420773456987" };
            Subscription subscription = new Subscription { Title = "Regular", Price = "2400", ProvidingGym = gym };

            gym.HasActivities.Add(activity);
            gym.HasFacilities.Add(facility);
            gym.HasSubscriptions.Add(subscription);

            context.Gyms.AddOrUpdate(gym);
            context.SaveChanges();
        }
    }
}
