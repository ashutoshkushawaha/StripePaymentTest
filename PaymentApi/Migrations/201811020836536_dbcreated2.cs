namespace PaymentApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbcreated2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Subscriptions");
            AddColumn("dbo.Subscriptions", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Subscriptions", "CustomerId", c => c.String());
            AddColumn("dbo.Subscriptions", "Start", c => c.DateTime());
            AddColumn("dbo.Subscriptions", "Expiry", c => c.DateTime());
            AddColumn("dbo.Subscriptions", "PlanId", c => c.String());
            AddColumn("dbo.Subscriptions", "NickName", c => c.String());
            AddColumn("dbo.Subscriptions", "Interval", c => c.String());
            AddColumn("dbo.Subscriptions", "ProductId", c => c.String());
            AlterColumn("dbo.Subscriptions", "SubscriptionId", c => c.String());
            AddPrimaryKey("dbo.Subscriptions", "Id");
            DropColumn("dbo.Subscriptions", "PurchaseDate");
            DropColumn("dbo.Subscriptions", "Expirty");
            DropColumn("dbo.Subscriptions", "PaymentSubscriptionId");
            DropColumn("dbo.Subscriptions", "ApiKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "ApiKey", c => c.Guid(nullable: false));
            AddColumn("dbo.Subscriptions", "PaymentSubscriptionId", c => c.String());
            AddColumn("dbo.Subscriptions", "Expirty", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subscriptions", "PurchaseDate", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.Subscriptions");
            AlterColumn("dbo.Subscriptions", "SubscriptionId", c => c.Guid(nullable: false));
            DropColumn("dbo.Subscriptions", "ProductId");
            DropColumn("dbo.Subscriptions", "Interval");
            DropColumn("dbo.Subscriptions", "NickName");
            DropColumn("dbo.Subscriptions", "PlanId");
            DropColumn("dbo.Subscriptions", "Expiry");
            DropColumn("dbo.Subscriptions", "Start");
            DropColumn("dbo.Subscriptions", "CustomerId");
            DropColumn("dbo.Subscriptions", "Id");
            AddPrimaryKey("dbo.Subscriptions", "SubscriptionId");
        }
    }
}
