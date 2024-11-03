namespace CastroMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class migracao2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Cars", new[] { "Category_CategoryId" });

            // Renomeia a coluna para CategoryId para garantir que ela exista antes de qualquer atualização
            RenameColumn(table: "dbo.Cars", name: "Category_CategoryId", newName: "CategoryId");

            // Ativa o IDENTITY_INSERT para permitir a inserção de um valor específico para CategoryId
            Sql("SET IDENTITY_INSERT Categories ON");

            // Insere a categoria padrão, caso ela não exista
            Sql("IF NOT EXISTS (SELECT * FROM Categories WHERE CategoryId = 1) " +
                "INSERT INTO Categories (CategoryId, Name) VALUES (1, 'Uncategorized')");

            // Desativa o IDENTITY_INSERT após a inserção
            Sql("SET IDENTITY_INSERT Categories OFF");

            // Agora que a coluna foi renomeada, podemos atualizar os registros NULL
            Sql("UPDATE Cars SET CategoryId = 1 WHERE CategoryId IS NULL");

            // Define a coluna CategoryId como NOT NULL
            AlterColumn("dbo.Cars", "CategoryId", c => c.Int(nullable: false));

            CreateIndex("dbo.Cars", "CategoryId");
            AddForeignKey("dbo.Cars", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Cars", new[] { "CategoryId" });

            // Reverte a coluna para permitir NULL
            AlterColumn("dbo.Cars", "CategoryId", c => c.Int());

            RenameColumn(table: "dbo.Cars", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.Cars", "Category_CategoryId");
            AddForeignKey("dbo.Cars", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
