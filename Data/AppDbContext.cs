using Microsoft.EntityFrameworkCore;
using PoupAI.Models;

namespace PoupAI.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            public DbSet<UsuarioModel> Usuarios { get; set; }
            public DbSet<ReceitaModel> Receitas { get; set; }
            public DbSet<DespesaModel> Despesas { get; set; }
            public DbSet<CategoriaModel> Categorias { get; set; }
            public DbSet<MetaModel> Metas { get; set; }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuration Usuario -> Receita
            modelBuilder.Entity<ReceitaModel>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Receitas)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            // Configuration Usuario -> Despesa
            modelBuilder.Entity<DespesaModel>()
                .HasOne(d => d.Usuario)
                .WithMany(u => u.Despesas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            //Configuration Categoria -> Despesa
            modelBuilder.Entity<DespesaModel>()
                .HasOne(d => d.Categoria)
                .WithMany(c => c.Despesas)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull);
            // Configuration Usuario -> Meta
            modelBuilder.Entity<MetaModel>()
            .HasOne(m => m.Usuario)
            .WithMany(u => u.Metas)
            .HasForeignKey(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
