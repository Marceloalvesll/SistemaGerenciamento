namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class segundamigracao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrePedidoes", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.Reservas", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.PrePedidoes", new[] { "Cliente_Id" });
            DropIndex("dbo.Reservas", new[] { "Cliente_Id" });
            DropColumn("dbo.PrePedidoes", "Cliente_Id");
            DropColumn("dbo.Reservas", "Cliente_Id");
            DropTable("dbo.Clientes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reservas", "Cliente_Id", c => c.Int());
            AddColumn("dbo.PrePedidoes", "Cliente_Id", c => c.Int());
            CreateIndex("dbo.Reservas", "Cliente_Id");
            CreateIndex("dbo.PrePedidoes", "Cliente_Id");
            AddForeignKey("dbo.Reservas", "Cliente_Id", "dbo.Clientes", "Id");
            AddForeignKey("dbo.PrePedidoes", "Cliente_Id", "dbo.Clientes", "Id");
        }
    }
}
