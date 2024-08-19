using Dominio;
using Memoria;

namespace Logica;

public class MonedaLogica
{
    private readonly IRepositorioMoneda<Moneda> _repositorio;

    public MonedaLogica(IRepositorioMoneda<Moneda> monedaRepositorio)
    {
        _repositorio = monedaRepositorio;
    }

    public Moneda AgregarMoneda(Moneda unaMoneda)
    {
        return _repositorio.Agregar(unaMoneda);
    }
    
    public Moneda? EncontrarMoneda(int idAEncontrar)
    {
        return _repositorio.Encontrar(x => x.Id == idAEncontrar);
    }

    public IList<Moneda> ListarMonedas()
    {
        return _repositorio.ListarTodos();
    }
}