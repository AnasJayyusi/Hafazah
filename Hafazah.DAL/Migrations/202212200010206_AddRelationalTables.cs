namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationalTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "EducationLevelId", c => c.Int());
            AddColumn("dbo.Instructors", "QuranMemorizedId", c => c.Int());
            AddColumn("dbo.Members", "EducationLevelId", c => c.Int());
            AddColumn("dbo.Members", "QuranMemorizedId", c => c.Int());
            CreateIndex("dbo.Instructors", "EducationLevelId");
            CreateIndex("dbo.Instructors", "QuranMemorizedId");
            CreateIndex("dbo.Members", "EducationLevelId");
            CreateIndex("dbo.Members", "QuranMemorizedId");
            AddForeignKey("dbo.Instructors", "EducationLevelId", "dbo.EducationLevels", "Id");
            AddForeignKey("dbo.Instructors", "QuranMemorizedId", "dbo.QuranMemorizeds", "Id");
            AddForeignKey("dbo.Members", "EducationLevelId", "dbo.EducationLevels", "Id");
            AddForeignKey("dbo.Members", "QuranMemorizedId", "dbo.QuranMemorizeds", "Id");
            DropColumn("dbo.Instructors", "EducationLevel");
            DropColumn("dbo.Instructors", "QuranMemorized");
            DropColumn("dbo.Members", "EducationLevel");
            DropColumn("dbo.Members", "QuranMemorized");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "QuranMemorized", c => c.String());
            AddColumn("dbo.Members", "EducationLevel", c => c.String(maxLength: 128));
            AddColumn("dbo.Instructors", "QuranMemorized", c => c.String());
            AddColumn("dbo.Instructors", "EducationLevel", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Members", "QuranMemorizedId", "dbo.QuranMemorizeds");
            DropForeignKey("dbo.Members", "EducationLevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.Instructors", "QuranMemorizedId", "dbo.QuranMemorizeds");
            DropForeignKey("dbo.Instructors", "EducationLevelId", "dbo.EducationLevels");
            DropIndex("dbo.Members", new[] { "QuranMemorizedId" });
            DropIndex("dbo.Members", new[] { "EducationLevelId" });
            DropIndex("dbo.Instructors", new[] { "QuranMemorizedId" });
            DropIndex("dbo.Instructors", new[] { "EducationLevelId" });
            DropColumn("dbo.Members", "QuranMemorizedId");
            DropColumn("dbo.Members", "EducationLevelId");
            DropColumn("dbo.Instructors", "QuranMemorizedId");
            DropColumn("dbo.Instructors", "EducationLevelId");
        }
    }
}
