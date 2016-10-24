namespace Emvisi.MigrationsBI
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GymInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.Gyms",
                c => new
                    {
                        GymId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        BrandName = c.String(),
                        LogoFilePath = c.String(),
                        ContactInfo_ContactInfoId = c.Int(),
                        Position_PositionId = c.Int(),
                    })
                .PrimaryKey(t => t.GymId)
                .ForeignKey("dbo.ContactInfoes", t => t.ContactInfo_ContactInfoId)
                .ForeignKey("dbo.Positions", t => t.Position_PositionId)
                .Index(t => t.ContactInfo_ContactInfoId)
                .Index(t => t.Position_PositionId);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ContactInfoId = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Email = c.String(),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactInfoId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Zip = c.String(),
                        City_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .Index(t => t.City_CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Region_RegionId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Regions", t => t.Region_RegionId)
                .Index(t => t.Region_RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RegionId);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        FacilityId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.FacilityId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Price = c.String(),
                        Gym_GymId = c.Int(),
                    })
                .PrimaryKey(t => t.SubscriptionId)
                .ForeignKey("dbo.Gyms", t => t.Gym_GymId)
                .Index(t => t.Gym_GymId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Gym_GymId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Gyms", t => t.Gym_GymId)
                .Index(t => t.Gym_GymId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        GeoLat = c.String(),
                        GeoLong = c.String(),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.GymActivity",
                c => new
                    {
                        GymId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GymId, t.ActivityId })
                .ForeignKey("dbo.Gyms", t => t.GymId, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.GymId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.GymFacility",
                c => new
                    {
                        GymId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GymId, t.FacilityId })
                .ForeignKey("dbo.Gyms", t => t.GymId, cascadeDelete: true)
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .Index(t => t.GymId)
                .Index(t => t.FacilityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gyms", "Position_PositionId", "dbo.Positions");
            DropForeignKey("dbo.Images", "Gym_GymId", "dbo.Gyms");
            DropForeignKey("dbo.Subscriptions", "Gym_GymId", "dbo.Gyms");
            DropForeignKey("dbo.GymFacility", "FacilityId", "dbo.Facilities");
            DropForeignKey("dbo.GymFacility", "GymId", "dbo.Gyms");
            DropForeignKey("dbo.GymActivity", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.GymActivity", "GymId", "dbo.Gyms");
            DropForeignKey("dbo.Gyms", "ContactInfo_ContactInfoId", "dbo.ContactInfoes");
            DropForeignKey("dbo.ContactInfoes", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Region_RegionId", "dbo.Regions");
            DropIndex("dbo.GymFacility", new[] { "FacilityId" });
            DropIndex("dbo.GymFacility", new[] { "GymId" });
            DropIndex("dbo.GymActivity", new[] { "ActivityId" });
            DropIndex("dbo.GymActivity", new[] { "GymId" });
            DropIndex("dbo.Images", new[] { "Gym_GymId" });
            DropIndex("dbo.Subscriptions", new[] { "Gym_GymId" });
            DropIndex("dbo.Cities", new[] { "Region_RegionId" });
            DropIndex("dbo.Addresses", new[] { "City_CityId" });
            DropIndex("dbo.ContactInfoes", new[] { "Address_AddressId" });
            DropIndex("dbo.Gyms", new[] { "Position_PositionId" });
            DropIndex("dbo.Gyms", new[] { "ContactInfo_ContactInfoId" });
            DropTable("dbo.GymFacility");
            DropTable("dbo.GymActivity");
            DropTable("dbo.Positions");
            DropTable("dbo.Images");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Facilities");
            DropTable("dbo.Regions");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.Gyms");
            DropTable("dbo.Activities");
        }
    }
}
