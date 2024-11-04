using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace SistemaGerenciamento.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }

        [Phone]
        public string Telefone { get; set; }

        public List<PrePedido> PrePedidos { get; set; } = new List<PrePedido>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<CategoriaDoMenu> CategoriaDoMenus { get; set; }
        public DbSet<PrePedido> PrePedidos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ItemDoMenu> ItensDoMenu { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<PrePedidoItemDoMenu> PrePedidoItens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
