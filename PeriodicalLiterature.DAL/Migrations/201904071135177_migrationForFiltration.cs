namespace PeriodicalLiterature.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class migrationForFiltration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Contract_Id", c => c.Guid());
            AddColumn("dbo.Contracts", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Comments", "Contract_Id");
            AddForeignKey("dbo.Comments", "Contract_Id", "dbo.Contracts", "Id");
            DropColumn("dbo.Editions", "Rating");
        }

        public override void Down()
        {
            AddColumn("dbo.Editions", "Rating", c => c.Double(nullable: false));
            DropForeignKey("dbo.Comments", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.Comments", new[] { "Contract_Id" });
            DropColumn("dbo.Contracts", "Rating");
            DropColumn("dbo.Comments", "Contract_Id");
        }
    }
}
