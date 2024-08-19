using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class ObjetivoDeGastoBDRepositorio : IRepositorioObjDeGasto<ObjetivoDeGasto>
{
    
    private SQLContexto _contexto;

    public ObjetivoDeGastoBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }
    public ObjetivoDeGasto Agregar(ObjetivoDeGasto unObjetivoDeGasto)
    {
        _contexto.ObjetivoDeGastos.Add(unObjetivoDeGasto);
        _contexto.SaveChanges();
        return unObjetivoDeGasto;
    }

    public ObjetivoDeGasto Encontrar(Func<ObjetivoDeGasto, bool> filtro)
    {
        return _contexto.ObjetivoDeGastos
            .Include(objetivoDeGasto => objetivoDeGasto.UsuarioCreador)
            .Include(objetivoDeGasto => objetivoDeGasto.Espacio)
            .Include(objetivoDeGasto => objetivoDeGasto.Categorias)
            .Where(filtro).FirstOrDefault();
    }

    public IList<ObjetivoDeGasto> ListarTodos()
    {
        return _contexto.ObjetivoDeGastos
            .Include(objetivoDeGasto => objetivoDeGasto.UsuarioCreador)
            .Include(objetivoDeGasto => objetivoDeGasto.Espacio)
            .Include(objetivoDeGasto => objetivoDeGasto.Categorias)
            .ToList();
    }
    
    public ObjetivoDeGasto? Actualizar(ObjetivoDeGasto unObjetivoDeGastoEditado)
    {
        ObjetivoDeGasto objetivoDeGastoAActualizar = 
            Encontrar(x => x.Id == unObjetivoDeGastoEditado.Id);
        
        if (objetivoDeGastoAActualizar != null)
        {
            objetivoDeGastoAActualizar.Token = unObjetivoDeGastoEditado.Token;
        }

        _contexto.SaveChanges();
        return objetivoDeGastoAActualizar;
    }
}