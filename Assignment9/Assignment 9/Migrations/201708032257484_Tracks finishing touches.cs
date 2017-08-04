namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tracksfinishingtouches : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "MediaContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Tracks", "Media", c => c.Binary());
            DropColumn("dbo.Tracks", "ClipContentType");
            DropColumn("dbo.Tracks", "SampleClip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "SampleClip", c => c.Binary());
            AddColumn("dbo.Tracks", "ClipContentType", c => c.String(maxLength: 200));
            DropColumn("dbo.Tracks", "Media");
            DropColumn("dbo.Tracks", "MediaContentType");
        }
    }
}
