namespace CastroMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "ImagePath");
        }
    }
}
