namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSurahName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LevelHomeworks", "SurahName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LevelHomeworks", "SurahName");
        }
    }
}
