namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizandocodprepedido : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemDoMenus", "PrePedido_Id", "dbo.PrePedidoes");
            DropIndex("dbo.ItemDoMenus", new[] { "PrePedido_Id" });
            CreateTable(
                "dbo.PrePedidoItemDoMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrePedidoId = c.Int(nullable: false),
                        ItemDoMenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemDoMenus", t => t.ItemDoMenuId, cascadeDelete: true)
                .ForeignKey("dbo.PrePedidoes", t => t.PrePedidoId, cascadeDelete: true)
                .Index(t => t.PrePedidoId)
                .Index(t => t.ItemDoMenuId);
            
            DropColumn("dbo.ItemDoMenus", "PrePedido_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemDoMenus", "PrePedido_Id", c => c.Int());
            DropForeignKey("dbo.PrePedidoItemDoMenus", "PrePedidoId", "dbo.PrePedidoes");
            DropForeignKey("dbo.PrePedidoItemDoMenus", "ItemDoMenuId", "dbo.ItemDoMenus");
            DropIndex("dbo.PrePedidoItemDoMenus", new[] { "ItemDoMenuId" });
            DropIndex("dbo.PrePedidoItemDoMenus", new[] { "PrePedidoId" });
            DropTable("dbo.PrePedidoItemDoMenus");
            CreateIndex("dbo.ItemDoMenus", "PrePedido_Id");
            AddForeignKey("dbo.ItemDoMenus", "PrePedido_Id", "dbo.PrePedidoes", "Id");
        }
    }
}
