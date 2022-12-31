namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWaveNumberColumnToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "WaveNumber", c => c.String());
            DropColumn("dbo.Members", "InterviewDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "InterviewDate", c => c.DateTime());
            DropColumn("dbo.Members", "WaveNumber");
        }
    }
}
