namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProgramStractureTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Levels", "Member_Id", "dbo.Members");
            DropIndex("dbo.Levels", new[] { "Member_Id" });
            CreateTable(
                "dbo.Paths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Duration = c.String(),
                        TotalNumber = c.Int(nullable: false),
                        RequiredWorkingDays = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequiredPagesToSubmit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        ProgramType = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhaseNumber = c.Int(),
                        PathId = c.Int(nullable: false),
                        TotalPageNumber = c.Int(),
                        PageFrom = c.Int(nullable: false),
                        PageTo = c.Int(nullable: false),
                        SurahFrom = c.String(maxLength: 50),
                        SurahTo = c.String(maxLength: 50),
                        QuranicVerseFrom = c.String(maxLength: 50),
                        QuranicVerseTo = c.String(maxLength: 50),
                        MaxNumberOfExcuses = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Paths", t => t.PathId, cascadeDelete: true)
                .Index(t => t.PathId);
            
            AddColumn("dbo.Levels", "PathId", c => c.Int(nullable: false));
            AddColumn("dbo.Levels", "QuranicVerseFrom", c => c.String(maxLength: 50));
            AddColumn("dbo.Levels", "QuranicVerseTo", c => c.String(maxLength: 50));
            AddColumn("dbo.Levels", "Description", c => c.String());
            AlterColumn("dbo.Levels", "LevelNumber", c => c.Int());
            AlterColumn("dbo.Levels", "TotalPageNumber", c => c.Int());
            CreateIndex("dbo.Levels", "PathId");
            AddForeignKey("dbo.Levels", "PathId", "dbo.Paths", "Id", cascadeDelete: true);
            DropColumn("dbo.Levels", "EstimatedTime");
            DropColumn("dbo.Levels", "Member_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Levels", "Member_Id", c => c.Int());
            AddColumn("dbo.Levels", "EstimatedTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Phases", "PathId", "dbo.Paths");
            DropForeignKey("dbo.Levels", "PathId", "dbo.Paths");
            DropIndex("dbo.Phases", new[] { "PathId" });
            DropIndex("dbo.Levels", new[] { "PathId" });
            AlterColumn("dbo.Levels", "TotalPageNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Levels", "LevelNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Levels", "Description");
            DropColumn("dbo.Levels", "QuranicVerseTo");
            DropColumn("dbo.Levels", "QuranicVerseFrom");
            DropColumn("dbo.Levels", "PathId");
            DropTable("dbo.Phases");
            DropTable("dbo.Paths");
            CreateIndex("dbo.Levels", "Member_Id");
            AddForeignKey("dbo.Levels", "Member_Id", "dbo.Members", "Id");
        }
    }
}
