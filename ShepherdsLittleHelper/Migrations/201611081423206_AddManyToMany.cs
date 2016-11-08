namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        MaxOccupancy = c.Int(nullable: false),
                        LocationNotes = c.String(),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.Groups", t => t.GroupID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetID = c.Int(nullable: false, identity: true),
                        PetName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Weight = c.Double(nullable: false),
                        PetNotes = c.String(),
                        ImageURL = c.String(),
                        LocationID = c.Int(nullable: false),
                        PetTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PetID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.PetTypes", t => t.PetTypeID)
                .Index(t => t.LocationID)
                .Index(t => t.PetTypeID);
            
            CreateTable(
                "dbo.PetTypes",
                c => new
                    {
                        PetTypeID = c.Int(nullable: false, identity: true),
                        PetTypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.PetTypeID);
            
            CreateTable(
                "dbo.PetTasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskDescription = c.String(),
                        Frequency = c.Double(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        LocationID = c.Int(nullable: false),
                        PetID = c.Int(nullable: false),
                        TaskTypeID = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.Pets", t => t.PetID)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeID)
                .Index(t => t.LocationID)
                .Index(t => t.PetID)
                .Index(t => t.TaskTypeID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskTypeName = c.String(),
                        TaskTypeNotes = c.String(),
                    })
                .PrimaryKey(t => t.TaskID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.T_Group_User",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupID, t.UserID })
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PetTasks", "TaskTypeID", "dbo.TaskTypes");
            DropForeignKey("dbo.PetTasks", "PetID", "dbo.Pets");
            DropForeignKey("dbo.PetTasks", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.PetTasks", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pets", "PetTypeID", "dbo.PetTypes");
            DropForeignKey("dbo.Pets", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.T_Group_User", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.T_Group_User", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.T_Group_User", new[] { "UserID" });
            DropIndex("dbo.T_Group_User", new[] { "GroupID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PetTasks", new[] { "ApplicationUserID" });
            DropIndex("dbo.PetTasks", new[] { "TaskTypeID" });
            DropIndex("dbo.PetTasks", new[] { "PetID" });
            DropIndex("dbo.PetTasks", new[] { "LocationID" });
            DropIndex("dbo.Pets", new[] { "PetTypeID" });
            DropIndex("dbo.Pets", new[] { "LocationID" });
            DropIndex("dbo.Locations", new[] { "GroupID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.T_Group_User");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TaskTypes");
            DropTable("dbo.PetTasks");
            DropTable("dbo.PetTypes");
            DropTable("dbo.Pets");
            DropTable("dbo.Locations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Groups");
        }
    }
}
