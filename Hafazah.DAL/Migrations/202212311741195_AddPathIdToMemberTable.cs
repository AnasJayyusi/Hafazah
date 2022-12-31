namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPathIdToMemberTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "PathId", c => c.Int(nullable: false));
            CreateIndex("dbo.Members", "PathId");
            AddForeignKey("dbo.Members", "PathId", "dbo.Paths", "Id", cascadeDelete: true);
            DropColumn("dbo.Members", "CurrentPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "CurrentPath", c => c.String());
            DropForeignKey("dbo.Members", "PathId", "dbo.Paths");
            DropIndex("dbo.Members", new[] { "PathId" });
            DropColumn("dbo.Members", "PathId");
        }
    }
}
