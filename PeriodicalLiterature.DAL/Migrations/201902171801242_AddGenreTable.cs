namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.GenreContracts",
                c => new
                    {
                        Genre_Id = c.Guid(nullable: false),
                        Contract_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Contract_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Contracts", t => t.Contract_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Contract_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenreContracts", "Contract_Id", "dbo.Contracts");
            DropForeignKey("dbo.GenreContracts", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreContracts", new[] { "Contract_Id" });
            DropIndex("dbo.GenreContracts", new[] { "Genre_Id" });
            DropIndex("dbo.Genres", new[] { "Name" });
            DropTable("dbo.GenreContracts");
            DropTable("dbo.Genres");
        }
    }
}
