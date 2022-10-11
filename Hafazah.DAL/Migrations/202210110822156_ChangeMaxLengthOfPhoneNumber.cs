namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMaxLengthOfPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "PhoneNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "PhoneNumber", c => c.String(maxLength: 10));
        }
    }
}
