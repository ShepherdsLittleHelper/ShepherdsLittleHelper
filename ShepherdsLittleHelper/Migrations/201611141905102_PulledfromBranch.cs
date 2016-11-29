namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PulledfromBranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PetTasks", "IsDone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PetTasks", "IsDone");
        }
    }
}
