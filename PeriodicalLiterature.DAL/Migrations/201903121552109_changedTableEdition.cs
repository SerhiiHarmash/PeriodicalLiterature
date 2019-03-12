namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedTableEdition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Editions", "CoverName", c => c.String());
            AddColumn("dbo.Editions", "FileName", c => c.String());
            DropColumn("dbo.Editions", "Status");
            DropColumn("dbo.Editions", "CoverId");
            DropColumn("dbo.Editions", "FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Editions", "FileId", c => c.Guid(nullable: false));
            AddColumn("dbo.Editions", "CoverId", c => c.Guid(nullable: false));
            AddColumn("dbo.Editions", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Editions", "FileName");
            DropColumn("dbo.Editions", "CoverName");
        }
    }
}
