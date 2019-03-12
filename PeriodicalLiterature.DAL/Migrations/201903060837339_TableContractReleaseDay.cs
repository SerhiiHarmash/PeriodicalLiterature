namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableContractReleaseDay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "NextReleaseDate", c => c.DateTime());
            AddColumn("dbo.Contracts", "ReleaseDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "ReleaseDay");
            DropColumn("dbo.Contracts", "NextReleaseDate");
        }
    }
}
