namespace ApiEstoque.Contexto;

using Microsoft.EntityFrameworkCore;
using ApiEstoque.Models;


public class BancoDados(DbContextOptions options) : DbContext(options)
{
    public DbSet<Usuario> TabelaUsuarios { get; set; }
    public DbSet<Categoria> TabelaCategorias { get; set; }
    public DbSet<Produto> TabelaProdutos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasMany(c => c.ListaProdutos).WithOne(p => p.Categoria).HasForeignKey(p => p.CategoriaId);
        base.OnModelCreating(modelBuilder);
    }

}