namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentConfirmationCodes", "SubscriptionId", "dbo.Subscriptions");
            DropIndex("dbo.PaymentConfirmationCodes", new[] { "SubscriptionId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.PaymentConfirmationCodes", "SubscriptionId");
            AddForeignKey("dbo.PaymentConfirmationCodes", "SubscriptionId", "dbo.Subscriptions", "Id", cascadeDelete: true);
        }
    }
}
