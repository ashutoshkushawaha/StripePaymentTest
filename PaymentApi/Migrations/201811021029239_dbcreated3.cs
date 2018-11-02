namespace PaymentApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbcreated3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        StripeProductId = c.String(),
                        PlanId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
