namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudandovariaveisemreservaemesa : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Mesas", "Numero", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Mesas", new[] { "Numero" });
        }
    }
}
