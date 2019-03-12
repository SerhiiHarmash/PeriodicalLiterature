namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Editions", "AddingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Editions", "ReleaseDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Editions", "Date");
            DropColumn("dbo.Contracts", "NextReleaseDate");
            DropColumn("dbo.Contracts", "ReleaseDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contracts", "ReleaseDay", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "NextReleaseDate", c => c.DateTime());
            AddColumn("dbo.Editions", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Editions", "ReleaseDate");
            DropColumn("dbo.Editions", "AddingDate");
        }
    }
}
