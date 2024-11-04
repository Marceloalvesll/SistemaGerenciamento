using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaGerenciamento.Models
{
    public partial class DBmodel : DbContext
    {
        public DBmodel()
            : base("name=DBmodel")
        {
        }

        public virtual DbSet<TBLimage> TBLimages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBLimage>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<TBLimage>()
                .Property(e => e.Image)
                .IsUnicode(false);
        }
    }
}
