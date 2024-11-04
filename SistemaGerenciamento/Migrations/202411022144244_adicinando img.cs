namespace SistemaGerenciamento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicinandoimg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemDoMenus", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemDoMenus", "Image");
        }
    }
}
