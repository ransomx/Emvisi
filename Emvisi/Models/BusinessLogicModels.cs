using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Emvisi.Models
{
    public class BIContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Facility> Facilities { get; set; }

        public BIContext(): base("name=DefaultConnection") // connection string in the application configuration file.
    {

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static BIContext Create()
        {
            return new BIContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gym>().
      HasMany(c => c.HasActivities).
      WithMany(p => p.Gyms).
      Map(
       m =>
       {
           m.MapLeftKey("GymId");
           m.MapRightKey("ActivityId");
           m.ToTable("GymActivity");
       });
            modelBuilder.Entity<Gym>().
      HasMany(c => c.HasFacilities).
      WithMany(p => p.Gyms).
      Map(
       m =>
       {
           m.MapLeftKey("GymId");
           m.MapRightKey("FacilityId");
           m.ToTable("GymFacility");
       });

            /*
             * modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        IMPORTANT: we are mapping the entity User to the same table as the entity ApplicationUser
        modelBuilder.Entity<User>().ToTable("User"); 
        http://stackoverflow.com/questions/28531201/entitytype-identityuserlogin-has-no-key-defined-define-the-key-for-this-entit
             * 
             */
        }

        public DbQuery<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }
    }

    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public Gym ProvidingGym { get; set; }
    }

    public class Facility
    {
        public int FacilityId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public ICollection<Gym> Gyms
        {
            get; set;
        }

        public Facility()
        {
            Gyms = new HashSet<Gym>();
        }
    }

    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }

    public class Activity
    {
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public ICollection<Gym> Gyms
        {
            get; set;
        }

        public Activity()
        {
            Gyms = new HashSet<Gym>();
        }
    }

    public class Gym
    {
        public int GymId { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Brand { get; set; }
        public Address Address { get; set; }

        public ICollection<Facility> HasFacilities
        {
            get; set;
        }

        public ICollection<Activity> HasActivities
        {
            get; set;
        }

        public ICollection<Subscription> HasSubscriptions
        {
            get; set;
        }

        public Gym()
        {
            HasFacilities = new HashSet<Facility>();
            HasActivities = new HashSet<Activity>();
            HasSubscriptions = new HashSet<Subscription>();
        }
    }
}
 
 