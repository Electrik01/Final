namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Oplata", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Oplata");
        }
    }
}
