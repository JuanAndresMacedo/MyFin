using Dominio;

namespace Memoria.BaseDeDatosRepositorios;

public class MonedaBDRepositorio : IRepositorioMoneda<Moneda>
{
    private SQLContexto _contexto;
    
    public MonedaBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }
    
    public Moneda Agregar(Moneda unaMoneda)
    {
        _contexto.Monedas.Add(unaMoneda);
        _contexto.SaveChanges();
        return unaMoneda;
    }

    public Moneda? Encontrar(Func<Moneda, bool> filtro)
    {
        return _contexto.Monedas.Where(filtro).FirstOrDefault();
    }

    public IList<Moneda> ListarTodos()
    {
        return _contexto.Monedas.ToList();
    }
}