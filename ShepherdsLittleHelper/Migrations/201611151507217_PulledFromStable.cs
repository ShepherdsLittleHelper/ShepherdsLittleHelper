namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PulledFromStable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PetTasks", new[] { "LocationID" });
            CreateTable(
                "dbo.LocationTasks",
                c => new
                    {
                        L_TaskID = c.Int(nullable: false, identity: true),
                        TaskDescription = c.String(),
                        Frequency = c.String(),
                        Deadline = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        LocationID = c.Int(nullable: false),
                        TaskTypeID = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.L_TaskID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeID)
                .Index(t => t.LocationID)
                .Index(t => t.TaskTypeID)
                .Index(t => t.ApplicationUserID);
            
            AddColumn("dbo.Pets", "Gender", c => c.String(maxLength: 1));
            AddColumn("dbo.Pets", "AgeYears", c => c.Int(nullable: false));
            AddColumn("dbo.Pets", "AgeMonths", c => c.Int(nullable: false));
            AddColumn("dbo.Pets", "Age", c => c.String());
            AddColumn("dbo.PetTasks", "IsDone", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PetTasks", "LocationID", c => c.Int());
            CreateIndex("dbo.PetTasks", "LocationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationTasks", "TaskTypeID", "dbo.TaskTypes");
            DropForeignKey("dbo.LocationTasks", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.LocationTasks", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.PetTasks", new[] { "LocationID" });
            DropIndex("dbo.LocationTasks", new[] { "ApplicationUserID" });
            DropIndex("dbo.LocationTasks", new[] { "TaskTypeID" });
            DropIndex("dbo.LocationTasks", new[] { "LocationID" });
            AlterColumn("dbo.PetTasks", "LocationID", c => c.Int(nullable: false));
            DropColumn("dbo.PetTasks", "IsDone");
            DropColumn("dbo.Pets", "Age");
            DropColumn("dbo.Pets", "AgeMonths");
            DropColumn("dbo.Pets", "AgeYears");
            DropColumn("dbo.Pets", "Gender");
            DropTable("dbo.LocationTasks");
            CreateIndex("dbo.PetTasks", "LocationID");
        }
    }
}
