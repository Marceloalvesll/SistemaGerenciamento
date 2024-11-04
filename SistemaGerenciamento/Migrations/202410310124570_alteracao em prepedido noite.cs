namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteracaoemprepedidonoite : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservas", "DataHoraReserva", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PrePedidoes", "DataHora", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.PrePedidoes", "DataHora", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservas", "DataHoraReserva", c => c.DateTime(nullable: false));
        }
    }
}
