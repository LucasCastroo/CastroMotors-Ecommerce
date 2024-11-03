namespace CastroMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsFinalized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsFinalized");
        }
    }
}
