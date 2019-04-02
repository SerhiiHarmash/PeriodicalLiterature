namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Subscriptions", name: "UserId", newName: "SubscriberId");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_UserId", newName: "IX_SubscriberId");
            CreateTable(
                "dbo.PaymentConfirmationCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CallBackCode = c.String(),
                        SubscriptionId = c.Guid(nullable: false),
                        CardNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionId, cascadeDelete: true)
                .Index(t => t.SubscriptionId);
            
            AddColumn("dbo.Subscriptions", "IsPayed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentConfirmationCodes", "SubscriptionId", "dbo.Subscriptions");
            DropIndex("dbo.PaymentConfirmationCodes", new[] { "SubscriptionId" });
            DropColumn("dbo.Subscriptions", "IsPayed");
            DropTable("dbo.PaymentConfirmationCodes");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_SubscriberId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Subscriptions", name: "SubscriberId", newName: "UserId");
        }
    }
}
