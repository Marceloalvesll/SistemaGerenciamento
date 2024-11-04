namespace SistemaGerenciamento.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using SistemaGerenciamento.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SistemaGerenciamento.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SistemaGerenciamento.Models.ApplicationDbContext";
        }

        protected override void Seed(SistemaGerenciamento.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Cria a role de administrador, se ainda não existir
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            // Cria um usuário administrador, se necessário
            var adminUser = userManager.FindByEmail("admin@teste.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = "admin@teste.com", Email = "admin@teste.com" };
                userManager.Create(adminUser, "SenhaForte123@"); 
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            // Salva mudanças
            context.SaveChanges();
        }
    }
}
