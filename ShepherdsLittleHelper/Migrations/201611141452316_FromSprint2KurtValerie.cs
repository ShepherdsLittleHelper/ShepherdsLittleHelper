namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FromSprint2KurtValerie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "AgeYears", c => c.Int(nullable: false));
            AddColumn("dbo.Pets", "AgeMonths", c => c.Int(nullable: false));
            AlterColumn("dbo.Pets", "Age", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pets", "Age", c => c.Double(nullable: false));
            DropColumn("dbo.Pets", "AgeMonths");
            DropColumn("dbo.Pets", "AgeYears");
        }
    }
}
