namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelTableWithRelationShips : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        SecondName = c.String(maxLength: 128),
                        ThirdName = c.String(maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 128),
                        Gender = c.Int(nullable: false),
                        BirthDate = c.DateTime(),
                        PhoneNumber = c.String(maxLength: 10),
                        CreatedDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelNumber = c.Int(nullable: false),
                        TotalPageNumber = c.Int(nullable: false),
                        PageFrom = c.Int(nullable: false),
                        PageTo = c.Int(nullable: false),
                        SurahFrom = c.String(maxLength: 50),
                        SurahTo = c.String(maxLength: 50),
                        EstimatedTime = c.DateTime(nullable: false),
                        MaxNumberOfExcuses = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Member_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
            AddColumn("dbo.Members", "SuggestPassword", c => c.String(maxLength: 50));
            AddColumn("dbo.Members", "ProgramType", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "CurrentPath", c => c.String());
            AddColumn("dbo.Members", "CurrentLevel", c => c.Int());
            AddColumn("dbo.Members", "WarningCounter", c => c.Int());
            AddColumn("dbo.Members", "AccomplishedPages", c => c.Int());
            AddColumn("dbo.Members", "LastSent", c => c.DateTime());
            AddColumn("dbo.Members", "InstrcutorId", c => c.Int());
            CreateIndex("dbo.Members", "InstrcutorId");
            AddForeignKey("dbo.Members", "InstrcutorId", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Levels", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.Members", "InstrcutorId", "dbo.Instructors");
            DropIndex("dbo.Levels", new[] { "Member_Id" });
            DropIndex("dbo.Members", new[] { "InstrcutorId" });
            DropColumn("dbo.Members", "InstrcutorId");
            DropColumn("dbo.Members", "LastSent");
            DropColumn("dbo.Members", "AccomplishedPages");
            DropColumn("dbo.Members", "WarningCounter");
            DropColumn("dbo.Members", "CurrentLevel");
            DropColumn("dbo.Members", "CurrentPath");
            DropColumn("dbo.Members", "ProgramType");
            DropColumn("dbo.Members", "SuggestPassword");
            DropTable("dbo.Levels");
            DropTable("dbo.Instructors");
        }
    }
}
