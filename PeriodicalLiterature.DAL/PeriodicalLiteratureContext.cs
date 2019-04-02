using Microsoft.AspNet.Identity.EntityFramework;
using PeriodicalLiterature.DAL.Identity;
using PeriodicalLiterature.Models.Entities;
using System.Data.Entity;

namespace PeriodicalLiterature.DAL
{
    public class PeriodicalLiteratureContext : IdentityDbContext<ApplicationUser>
    {
        public PeriodicalLiteratureContext() : base("PeriodicalLiteratureContext") { }

        public static PeriodicalLiteratureContext Create()
        {
            return new PeriodicalLiteratureContext();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Edition> Editions { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ContractResult> ContractResults { get; set; }

        public DbSet<EditionResult> EditionResults { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<BankTransaction> BankTransactions { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<PaymentConfirmationCode> PaymentConfirmationCodes { get; set; }
    }
}
