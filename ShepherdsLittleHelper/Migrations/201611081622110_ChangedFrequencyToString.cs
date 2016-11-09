namespace ShepherdsLittleHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedFrequencyToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PetTasks", "Frequency", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PetTasks", "Frequency", c => c.Double(nullable: false));
        }
    }
}
