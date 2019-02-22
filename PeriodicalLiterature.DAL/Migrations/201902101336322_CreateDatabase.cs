namespace PeriodicalLiterature.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Body = c.String(),
                    UserId = c.Guid(nullable: false),
                    ParentCommentId = c.Guid(),
                    IsDeleted = c.Boolean(nullable: false),
                    EditionId = c.Guid(nullable: false),
                    IsQuote = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Editions", t => t.EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ParentCommentId)
                .Index(t => t.EditionId);

            CreateTable(
                "dbo.Editions",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Status = c.Int(nullable: false),
                    ContractId = c.Guid(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Number = c.Byte(nullable: false),
                    Description = c.String(),
                    Pages = c.Byte(nullable: false),
                    CoverId = c.Guid(nullable: false),
                    FileId = c.Guid(nullable: false),
                    Rating = c.Double(nullable: false),
                    Readership = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);

            CreateTable(
                "dbo.Contracts",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    EditionTitle = c.String(),
                    CoverId = c.Guid(nullable: false),
                    Category = c.Int(nullable: false),
                    Language = c.Int(nullable: false),
                    Periodicity = c.Int(nullable: false),
                    DescriptionEdition = c.String(),
                    ReleasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    FileId = c.Guid(nullable: false),
                    Status = c.Int(nullable: false),
                    PublisherId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .Index(t => t.PublisherId);

            CreateTable(
                "dbo.Subscriptions",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    UserId = c.Guid(nullable: false),
                    ContractId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.Subscriber", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ContractId);

            CreateTable(
                "dbo.ContractResults",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    AdminId = c.Guid(nullable: false),
                    ContractId = c.Guid(nullable: false),
                    Status = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Message = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.AdminId)
                .Index(t => t.ContractId);

            CreateTable(
                "dbo.EditionResults",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    AdminId = c.Guid(nullable: false),
                    EditionId = c.Guid(nullable: false),
                    Status = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Message = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Editions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.AdminId)
                .Index(t => t.EditionId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Admins",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    SecondName = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Publisher",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Country = c.Int(nullable: false),
                    Description = c.String(),
                    Account = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PlannedAccount = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Subscriber",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    SecondName = c.String(),
                    Account = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Subscriber", "Id", "dbo.Users");
            DropForeignKey("dbo.Publisher", "Id", "dbo.Users");
            DropForeignKey("dbo.Admins", "Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EditionResults", "EditionId", "dbo.Editions");
            DropForeignKey("dbo.EditionResults", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.ContractResults", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractResults", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Subscriptions", "UserId", "dbo.Subscriber");
            DropForeignKey("dbo.Subscriptions", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Contracts", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Editions", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Comments", "EditionId", "dbo.Editions");
            DropIndex("dbo.Subscriber", new[] { "Id" });
            DropIndex("dbo.Publisher", new[] { "Id" });
            DropIndex("dbo.Admins", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EditionResults", new[] { "EditionId" });
            DropIndex("dbo.EditionResults", new[] { "AdminId" });
            DropIndex("dbo.ContractResults", new[] { "ContractId" });
            DropIndex("dbo.ContractResults", new[] { "AdminId" });
            DropIndex("dbo.Subscriptions", new[] { "ContractId" });
            DropIndex("dbo.Subscriptions", new[] { "UserId" });
            DropIndex("dbo.Contracts", new[] { "PublisherId" });
            DropIndex("dbo.Editions", new[] { "ContractId" });
            DropIndex("dbo.Comments", new[] { "EditionId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.Subscriber");
            DropTable("dbo.Publisher");
            DropTable("dbo.Admins");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EditionResults");
            DropTable("dbo.ContractResults");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Contracts");
            DropTable("dbo.Editions");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
        }
    }
}
