namespace Hafazah.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetailsColumnsForInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "OtherDetails", c => c.String());
            AddColumn("dbo.Instructors", "EducationLevel", c => c.String(maxLength: 128));
            AddColumn("dbo.Instructors", "Username", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Instructors", "SuggestPassword", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Instructors", "Email", c => c.String(maxLength: 128));
            AddColumn("dbo.Instructors", "QuranMemorized", c => c.String());
            AddColumn("dbo.Instructors", "ProgramType", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "PhoneNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "PhoneNumber", c => c.String(maxLength: 10));
            DropColumn("dbo.Instructors", "ProgramType");
            DropColumn("dbo.Instructors", "QuranMemorized");
            DropColumn("dbo.Instructors", "Email");
            DropColumn("dbo.Instructors", "SuggestPassword");
            DropColumn("dbo.Instructors", "Username");
            DropColumn("dbo.Instructors", "EducationLevel");
            DropColumn("dbo.Instructors", "OtherDetails");
        }
    }
}
