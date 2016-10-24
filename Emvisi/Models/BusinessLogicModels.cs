using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Emvisi.Models
{
    public class BIContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }

        public BIContext(): base("name=DefaultConnection") // connection string in the application configuration file.
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
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
        public virtual Gym Gym { get; set; }
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
        public virtual City City { get; set; }
        public string Zip { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public virtual Region Region { get; set; }
    }

    public class Region
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities
        {
            get; set;
        }
        public Region()
        {
            Cities = new HashSet<City>();
        }
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

    public class Image
    {
        public int ImageId { get; set; }
        public string Path { get; set; }
        public virtual Gym Gym { get; set; }
    }

    public class Position
    {
        public int PositionId { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
    }

    public class ContactInfo
    {
        public int ContactInfoId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual Address Address { get; set; }
    }

    public class Gym
    {
        public int GymId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string LogoFilePath { get; set; }

        public virtual Position Position { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }

        public virtual ICollection<Image> Images
        {
            get; set;
        }
        public virtual ICollection<Facility> HasFacilities
        {
            get; set;
        }
        public virtual ICollection<Activity> HasActivities
        {
            get; set;
        }
        public virtual ICollection<Subscription> HasSubscriptions
        {
            get; set;
        }

        public Gym()
        {
            HasFacilities = new HashSet<Facility>();
            HasActivities = new HashSet<Activity>();
            HasSubscriptions = new HashSet<Subscription>();
            Images = new HashSet<Image>();
        }
    }
}
 
 