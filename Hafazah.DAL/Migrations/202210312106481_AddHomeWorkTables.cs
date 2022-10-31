namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHomeWorkTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LevelHomeworks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequiredMemorizedFrom = c.Int(),
                        RequiredMemorizedTo = c.Int(),
                        AbilityConnectFrom = c.Int(),
                        AbilityConnectTo = c.Int(),
                        ReviewFrom = c.Int(),
                        ReviewTo = c.Int(),
                        LevelId = c.Int(),
                        CreatedDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.PhaseHomeworks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequiredMemorizedFrom = c.Int(),
                        RequiredMemorizedTo = c.Int(),
                        AbilityConnectFrom = c.Int(),
                        AbilityConnectTo = c.Int(),
                        ReviewFrom = c.Int(),
                        ReviewTo = c.Int(),
                        PhaseId = c.Int(),
                        CreatedDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phases", t => t.PhaseId)
                .Index(t => t.PhaseId);
            
            AddColumn("dbo.Levels", "HomeWorkTotalCount", c => c.Int(nullable: false));
            AddColumn("dbo.Phases", "HomeWorkTotalCount", c => c.Int(nullable: false));
            DropColumn("dbo.Levels", "QuranicVerseFrom");
            DropColumn("dbo.Levels", "QuranicVerseTo");
            DropColumn("dbo.Phases", "QuranicVerseFrom");
            DropColumn("dbo.Phases", "QuranicVerseTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phases", "QuranicVerseTo", c => c.String(maxLength: 50));
            AddColumn("dbo.Phases", "QuranicVerseFrom", c => c.String(maxLength: 50));
            AddColumn("dbo.Levels", "QuranicVerseTo", c => c.String(maxLength: 50));
            AddColumn("dbo.Levels", "QuranicVerseFrom", c => c.String(maxLength: 50));
            DropForeignKey("dbo.PhaseHomeworks", "PhaseId", "dbo.Phases");
            DropForeignKey("dbo.LevelHomeworks", "LevelId", "dbo.Levels");
            DropIndex("dbo.PhaseHomeworks", new[] { "PhaseId" });
            DropIndex("dbo.LevelHomeworks", new[] { "LevelId" });
            DropColumn("dbo.Phases", "HomeWorkTotalCount");
            DropColumn("dbo.Levels", "HomeWorkTotalCount");
            DropTable("dbo.PhaseHomeworks");
            DropTable("dbo.LevelHomeworks");
        }
    }
}
