namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PulledFromSprint2kurtValerie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Gender", c => c.String(maxLength: 1));
            AddColumn("dbo.Pets", "AgeYears", c => c.Int(nullable: false));
            AddColumn("dbo.Pets", "AgeMonths", c => c.Int(nullable: false));
            AddColumn("dbo.Pets", "Age", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Age");
            DropColumn("dbo.Pets", "AgeMonths");
            DropColumn("dbo.Pets", "AgeYears");
            DropColumn("dbo.Pets", "Gender");
        }
    }
}
