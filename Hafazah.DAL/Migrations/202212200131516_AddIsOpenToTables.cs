namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsOpenToTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Levels", "IsOpenToRegistration", c => c.Boolean(nullable: false));
            AddColumn("dbo.Paths", "IsOpenToRegistration", c => c.Boolean(nullable: false));
            AddColumn("dbo.Phases", "IsOpenToRegistration", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Phases", "IsOpenToRegistration");
            DropColumn("dbo.Paths", "IsOpenToRegistration");
            DropColumn("dbo.Levels", "IsOpenToRegistration");
        }
    }
}
