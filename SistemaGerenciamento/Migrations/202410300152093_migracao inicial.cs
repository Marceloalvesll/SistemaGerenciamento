namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracaoinicial : DbMigration
    {
        public override void Up()
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
            
            CreateTable(
                "dbo.PrePedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataHora = c.DateTime(nullable: false),
                        Status = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cliente_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Cliente_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ItemDoMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Disponivel = c.Boolean(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        PrePedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaDoMenus", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.PrePedidoes", t => t.PrePedido_Id)
                .Index(t => t.CategoriaId)
                .Index(t => t.PrePedido_Id);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataHoraReserva = c.DateTime(nullable: false),
                        QuantidadePessoas = c.Int(nullable: false),
                        MesaId = c.Int(nullable: false),
                        Cliente_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mesas", t => t.MesaId, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.MesaId)
                .Index(t => t.Cliente_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Mesas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        Capacidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Telefone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrePedidoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservas", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.Reservas", "MesaId", "dbo.Mesas");
            DropForeignKey("dbo.PrePedidoes", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.ItemDoMenus", "PrePedido_Id", "dbo.PrePedidoes");
            DropForeignKey("dbo.ItemDoMenus", "CategoriaId", "dbo.CategoriaDoMenus");
            DropIndex("dbo.Reservas", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reservas", new[] { "Cliente_Id" });
            DropIndex("dbo.Reservas", new[] { "MesaId" });
            DropIndex("dbo.ItemDoMenus", new[] { "PrePedido_Id" });
            DropIndex("dbo.ItemDoMenus", new[] { "CategoriaId" });
            DropIndex("dbo.PrePedidoes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PrePedidoes", new[] { "Cliente_Id" });
            DropColumn("dbo.AspNetUsers", "Telefone");
            DropColumn("dbo.AspNetUsers", "Nome");
            DropTable("dbo.Mesas");
            DropTable("dbo.Reservas");
            DropTable("dbo.ItemDoMenus");
            DropTable("dbo.PrePedidoes");
            DropTable("dbo.Clientes");
        }
    }
}
