using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria;

public class SQLContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Espacio> Espacios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<TarjetaDeCredito> TarjetasDeCredito { get; set; }
    public DbSet<Monetaria> Monetarias { get; set; }
    public DbSet<TipoDeCambio> TiposDeCambios { get; set; }
    public DbSet<Transaccion> Transacciones { get; set; }
    public DbSet<ObjetivoDeGasto> ObjetivoDeGastos { get; set; }
    public DbSet<Moneda> Monedas { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.EspaciosQueAdministra)
            .WithOne(e => e.Administrador)
            .HasForeignKey(e => e.AdministradorId);
        
        base.OnModelCreating(modelBuilder);
    }
    
    public SQLContexto(DbContextOptions<SQLContexto> options) : base(options)
    {
        if (!Database.IsInMemory())
        {
            //Database.Migrate(); 
        }
    }
}