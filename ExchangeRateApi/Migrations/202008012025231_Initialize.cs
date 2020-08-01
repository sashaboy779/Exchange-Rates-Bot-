using System;
using System.Data.Entity.Migrations;

namespace ExchangeRateApi.Migrations
{
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCurrencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Currency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserTelegramId = c.Int(nullable: false),
                        LanguageCode = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserTelegramId, unique: true);
            
            CreateTable(
                "dbo.UserUserCurrencies",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserCurrency_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserCurrency_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserCurrencies", t => t.UserCurrency_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserCurrency_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUserCurrencies", "UserCurrency_Id", "dbo.UserCurrencies");
            DropForeignKey("dbo.UserUserCurrencies", "User_Id", "dbo.Users");
            DropIndex("dbo.UserUserCurrencies", new[] { "UserCurrency_Id" });
            DropIndex("dbo.UserUserCurrencies", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "UserTelegramId" });
            DropTable("dbo.UserUserCurrencies");
            DropTable("dbo.Users");
            DropTable("dbo.UserCurrencies");
        }
    }
}
