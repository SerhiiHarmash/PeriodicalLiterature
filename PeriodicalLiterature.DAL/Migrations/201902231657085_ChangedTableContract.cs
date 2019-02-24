namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTableContract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "CoverName", c => c.String());
            AddColumn("dbo.Contracts", "FileName", c => c.String());
            DropColumn("dbo.Contracts", "CoverId");
            DropColumn("dbo.Contracts", "FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contracts", "FileId", c => c.Guid(nullable: false));
            AddColumn("dbo.Contracts", "CoverId", c => c.Guid(nullable: false));
            DropColumn("dbo.Contracts", "FileName");
            DropColumn("dbo.Contracts", "CoverName");
        }
    }
}
