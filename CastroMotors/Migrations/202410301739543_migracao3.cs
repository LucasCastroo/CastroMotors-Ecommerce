namespace CastroMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "CountryOfOrigin", c => c.String(maxLength: 50));
            AddColumn("dbo.Brands", "FoundedYear", c => c.Int());
            AddColumn("dbo.Cars", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "Color", c => c.String(maxLength: 50));
            AddColumn("dbo.Cars", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Cars", "Description", c => c.String());
            AddColumn("dbo.Categories", "Description", c => c.String(maxLength: 200));
            AddColumn("dbo.Categories", "Code", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Code");
            DropColumn("dbo.Categories", "Description");
            DropColumn("dbo.Cars", "Description");
            DropColumn("dbo.Cars", "Price");
            DropColumn("dbo.Cars", "Color");
            DropColumn("dbo.Cars", "Year");
            DropColumn("dbo.Brands", "FoundedYear");
            DropColumn("dbo.Brands", "CountryOfOrigin");
        }
    }
}
