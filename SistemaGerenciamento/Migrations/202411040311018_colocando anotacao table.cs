namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colocandoanotacaotable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CategoriaDoMenus", newName: "TBLcategoria");
            RenameTable(name: "dbo.ItemDoMenus", newName: "TBLitemdomenu");
            RenameTable(name: "dbo.Mesas", newName: "TBLmesa");
            RenameTable(name: "dbo.Reservas", newName: "TBLreserva");
            RenameTable(name: "dbo.PrePedidoes", newName: "TBLprepedido");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TBLprepedido", newName: "PrePedidoes");
            RenameTable(name: "dbo.TBLreserva", newName: "Reservas");
            RenameTable(name: "dbo.TBLmesa", newName: "Mesas");
            RenameTable(name: "dbo.TBLitemdomenu", newName: "ItemDoMenus");
            RenameTable(name: "dbo.TBLcategoria", newName: "CategoriaDoMenus");
        }
    }
}
