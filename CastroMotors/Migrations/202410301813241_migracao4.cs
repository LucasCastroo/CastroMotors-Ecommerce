﻿namespace CastroMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "RowVersion");
        }
    }
}
