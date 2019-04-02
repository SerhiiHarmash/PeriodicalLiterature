namespace PeriodicalLiterature.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateToBanckTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankTransactions", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankTransactions", "Date");
        }
    }
}
