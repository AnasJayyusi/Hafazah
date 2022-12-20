namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveColumnToInsturactorTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "IsActive");
        }
    }
}
