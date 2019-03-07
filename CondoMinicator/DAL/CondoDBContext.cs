using CondoMinicator.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CondoMinicator.DAL
{
    public class CondoDBContext : DbContext
    {

        public CondoDBContext() : base("CondoDBContext")
        {
        }

        public DbSet<Administradora> Administradoras { get; set; }
        public DbSet<Condominio> Condominios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Usuario_Tipo> Usuario_Tipos { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Comunicado> Comunicados { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //desabilita referencia circular:
            modelBuilder.Entity<Usuario>()
                .HasRequired<Usuario_Tipo>(p => p.Usuario_Tipo)
                .WithMany()
                .WillCascadeOnDelete(false);

            //desabilita referencia circular:
            modelBuilder.Entity<Comunicado>()
                .HasRequired<Usuario>(p => p.De)
                .WithMany()
                .WillCascadeOnDelete(false);

            //desabilita referencia circular:
            modelBuilder.Entity<Comunicado>()
                .HasRequired<Usuario>(p => p.Para)
                .WithMany()
                .WillCascadeOnDelete(false);

        }
    }
}