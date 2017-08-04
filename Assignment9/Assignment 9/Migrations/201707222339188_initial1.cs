namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Depiction", c => c.String(maxLength: 250));
            AddColumn("dbo.Artists", "Portrayal", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Portrayal");
            DropColumn("dbo.Albums", "Depiction");
        }
    }
}
