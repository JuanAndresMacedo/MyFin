using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria;

public class SQLContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Espacio> Espacios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<TarjetaDeCredito> TarjetaDeCreditos { get; set; }
    public DbSet<Monetaria> Monetarias { get; set; }
    public DbSet<TipoDeCambio> TiposDeCambios { get; set; }
    public DbSet<Transaccion> Transacciones { get; set; }
    public DbSet<ObjetivoDeGasto> ObjetivoDeGastos { get; set; }
    
    public SQLContexto(DbContextOptions<SQLContexto> options) : base(options)
    {
        Database.Migrate();
    }
}