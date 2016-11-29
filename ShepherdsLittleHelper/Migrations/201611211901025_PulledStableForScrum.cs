namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PulledStableForScrum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocationTasks", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LocationTasks", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.PetTasks", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PetTasks", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PetTasks", "IsArchived");
            DropColumn("dbo.PetTasks", "CreationDate");
            DropColumn("dbo.LocationTasks", "IsArchived");
            DropColumn("dbo.LocationTasks", "CreationDate");
        }
    }
}
