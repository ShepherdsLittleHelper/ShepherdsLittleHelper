namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pending : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Gender", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Gender");
        }
    }
}
