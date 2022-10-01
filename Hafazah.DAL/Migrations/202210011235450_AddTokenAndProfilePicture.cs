namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTokenAndProfilePicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "NotificationToken", c => c.String());
            AddColumn("dbo.Members", "ProfilePictureBase64", c => c.String());
            AlterColumn("dbo.Members", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Members", "SuggestPassword", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "SuggestPassword", c => c.String(maxLength: 50));
            AlterColumn("dbo.Members", "Username", c => c.String(maxLength: 50));
            DropColumn("dbo.Members", "ProfilePictureBase64");
            DropColumn("dbo.Members", "NotificationToken");
        }
    }
}
