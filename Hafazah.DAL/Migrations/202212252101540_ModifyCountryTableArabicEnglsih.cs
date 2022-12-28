namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCountryTableArabicEnglsih : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "IsoName", c => c.String());
            AddColumn("dbo.Countries", "NameEn", c => c.String());
            AddColumn("dbo.Countries", "NameAr", c => c.String());
            AddColumn("dbo.Countries", "Iso3Name", c => c.String());
            AddColumn("dbo.Countries", "CodeNumber", c => c.String());
            AddColumn("dbo.Countries", "CountryCode", c => c.String());
            AddColumn("dbo.Members", "PreferenceTime", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "CountryId", c => c.String());
            AddColumn("dbo.Members", "Country_Id", c => c.Int());
            CreateIndex("dbo.Members", "Country_Id");
            AddForeignKey("dbo.Members", "Country_Id", "dbo.Countries", "Id");
            DropColumn("dbo.Members", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Country", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Members", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Members", new[] { "Country_Id" });
            DropColumn("dbo.Members", "Country_Id");
            DropColumn("dbo.Members", "CountryId");
            DropColumn("dbo.Members", "PreferenceTime");
            DropColumn("dbo.Countries", "CountryCode");
            DropColumn("dbo.Countries", "CodeNumber");
            DropColumn("dbo.Countries", "Iso3Name");
            DropColumn("dbo.Countries", "NameAr");
            DropColumn("dbo.Countries", "NameEn");
            DropColumn("dbo.Countries", "IsoName");
        }
    }
}
