namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterandoaccountController : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Nome", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Telefone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Telefone", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Nome", c => c.String(nullable: false));
        }
    }
}
