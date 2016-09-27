namespace Emvisi.MigrationsBI
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusinessInteligenceEssentials : DbMigration
    {
        public override void Up()
        {/*
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
                        Phone = c.String(),
                        Brand = c.String(),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.GymId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
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
                        ProvidingGym_GymId = c.Int(),
                    })
                .PrimaryKey(t => t.SubscriptionId)
                .ForeignKey("dbo.Gyms", t => t.ProvidingGym_GymId)
                .Index(t => t.ProvidingGym_GymId);
            
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
                .Index(t => t.FacilityId);*/
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "ProvidingGym_GymId", "dbo.Gyms");
            DropForeignKey("dbo.GymFacility", "FacilityId", "dbo.Facilities");
            DropForeignKey("dbo.GymFacility", "GymId", "dbo.Gyms");
            DropForeignKey("dbo.GymActivity", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.GymActivity", "GymId", "dbo.Gyms");
            DropForeignKey("dbo.Gyms", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.GymFacility", new[] { "FacilityId" });
            DropIndex("dbo.GymFacility", new[] { "GymId" });
            DropIndex("dbo.GymActivity", new[] { "ActivityId" });
            DropIndex("dbo.GymActivity", new[] { "GymId" });
            DropIndex("dbo.Subscriptions", new[] { "ProvidingGym_GymId" });
            DropIndex("dbo.Gyms", new[] { "Address_AddressId" });
            DropTable("dbo.GymFacility");
            DropTable("dbo.GymActivity");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Facilities");
            DropTable("dbo.Addresses");
            DropTable("dbo.Gyms");
            DropTable("dbo.Activities");
        }
    }
}
