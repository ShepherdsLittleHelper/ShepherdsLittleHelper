namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PulledfromBranch2 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationTasks", "TaskTypeID", "dbo.TaskTypes");
            DropForeignKey("dbo.LocationTasks", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.LocationTasks", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.LocationTasks", new[] { "ApplicationUserID" });
            DropIndex("dbo.LocationTasks", new[] { "TaskTypeID" });
            DropIndex("dbo.LocationTasks", new[] { "LocationID" });
            DropTable("dbo.LocationTasks");
        }
    }
}
