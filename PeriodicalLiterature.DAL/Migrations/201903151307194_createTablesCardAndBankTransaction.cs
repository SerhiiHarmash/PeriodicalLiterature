namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTablesCardAndBankTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankTransactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        From = c.String(),
                        To = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CardHolderName = c.String(),
                        CardNumber = c.String(),
                        DateOfExpiry = c.String(),
                        CVV = c.String(),
                        SubscriberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscriber", t => t.SubscriberId)
                .Index(t => t.SubscriberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "SubscriberId", "dbo.Subscriber");
            DropForeignKey("dbo.BankTransactions", "UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "SubscriberId" });
            DropIndex("dbo.BankTransactions", new[] { "UserId" });
            DropTable("dbo.Cards");
            DropTable("dbo.BankTransactions");
        }
    }
}
