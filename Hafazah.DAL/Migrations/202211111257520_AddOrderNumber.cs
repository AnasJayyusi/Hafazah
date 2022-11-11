namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LevelHomeworks", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.PhaseHomeworks", "OrderNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhaseHomeworks", "OrderNumber");
            DropColumn("dbo.LevelHomeworks", "OrderNumber");
        }
    }
}
