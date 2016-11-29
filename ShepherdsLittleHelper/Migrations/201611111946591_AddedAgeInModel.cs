namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAgeInModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Age", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Age");
        }
    }
}
