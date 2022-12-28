namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetAllColumnsRequiredInMemberTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Members", "EducationLevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.Members", "QuranMemorizedId", "dbo.QuranMemorizeds");
            DropIndex("dbo.Members", new[] { "EducationLevelId" });
            DropIndex("dbo.Members", new[] { "QuranMemorizedId" });
            DropIndex("dbo.Members", new[] { "Country_Id" });
            DropColumn("dbo.Members", "CountryId");
            RenameColumn(table: "dbo.Members", name: "Country_Id", newName: "CountryId");
            AlterColumn("dbo.Members", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Members", "Address", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Members", "JobTitle", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Members", "PhoneNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Members", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Members", "KnownFrom", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Members", "EducationLevelId", c => c.Int(nullable: false));
            AlterColumn("dbo.Members", "QuranMemorizedId", c => c.Int(nullable: false));
            AlterColumn("dbo.Members", "CountryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Members", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Members", "EducationLevelId");
            CreateIndex("dbo.Members", "QuranMemorizedId");
            CreateIndex("dbo.Members", "CountryId");
            AddForeignKey("dbo.Members", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Members", "EducationLevelId", "dbo.EducationLevels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Members", "QuranMemorizedId", "dbo.QuranMemorizeds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "QuranMemorizedId", "dbo.QuranMemorizeds");
            DropForeignKey("dbo.Members", "EducationLevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.Members", "CountryId", "dbo.Countries");
            DropIndex("dbo.Members", new[] { "CountryId" });
            DropIndex("dbo.Members", new[] { "QuranMemorizedId" });
            DropIndex("dbo.Members", new[] { "EducationLevelId" });
            AlterColumn("dbo.Members", "CountryId", c => c.Int());
            AlterColumn("dbo.Members", "CountryId", c => c.String());
            AlterColumn("dbo.Members", "QuranMemorizedId", c => c.Int());
            AlterColumn("dbo.Members", "EducationLevelId", c => c.Int());
            AlterColumn("dbo.Members", "KnownFrom", c => c.String(maxLength: 256));
            AlterColumn("dbo.Members", "Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.Members", "PhoneNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.Members", "JobTitle", c => c.String(maxLength: 128));
            AlterColumn("dbo.Members", "Address", c => c.String(maxLength: 256));
            AlterColumn("dbo.Members", "BirthDate", c => c.DateTime());
            RenameColumn(table: "dbo.Members", name: "CountryId", newName: "Country_Id");
            AddColumn("dbo.Members", "CountryId", c => c.String());
            CreateIndex("dbo.Members", "Country_Id");
            CreateIndex("dbo.Members", "QuranMemorizedId");
            CreateIndex("dbo.Members", "EducationLevelId");
            AddForeignKey("dbo.Members", "QuranMemorizedId", "dbo.QuranMemorizeds", "Id");
            AddForeignKey("dbo.Members", "EducationLevelId", "dbo.EducationLevels", "Id");
            AddForeignKey("dbo.Members", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
