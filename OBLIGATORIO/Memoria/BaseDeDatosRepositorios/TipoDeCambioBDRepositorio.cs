using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class TipoDeCambioBDRepositorio: IRepositorio<TipoDeCambio>
{
    private SQLContexto _contexto;

    public TipoDeCambioBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }
    public TipoDeCambio Agregar(TipoDeCambio unTipoDeCambio)
    {
        _contexto.TiposDeCambios.Add(unTipoDeCambio);
        _contexto.SaveChanges();
        return unTipoDeCambio;
    }
    
    public TipoDeCambio? Encontrar(Func<TipoDeCambio, bool> filtro)
    {
        return _contexto.TiposDeCambios
            .Include(tipoDeCambio => tipoDeCambio.UsuarioCreador)
            .Include(tipoDeCambio => tipoDeCambio.Espacio)
            .Include(tipoDeCambio => tipoDeCambio.Moneda)
            .Where(filtro).FirstOrDefault();
    }

    public IList<TipoDeCambio> ListarTodos()
    {
        return _contexto.TiposDeCambios
            .Include(tipoDeCambio => tipoDeCambio.UsuarioCreador)
            .Include(tipoDeCambio => tipoDeCambio.Espacio)
            .Include(tipoDeCambio => tipoDeCambio.Moneda)
            .ToList();
    }

    public TipoDeCambio? Eliminar(int idAEliminar)
    {
        TipoDeCambio tipoDeCambioAEliminar = 
            Encontrar(categoria => categoria.Id == idAEliminar);
        
        if (tipoDeCambioAEliminar != null)
        {
            _contexto.TiposDeCambios.RemoveRange(tipoDeCambioAEliminar);
        }
        _contexto.SaveChanges();
        return tipoDeCambioAEliminar;
    }

    public TipoDeCambio? Actualizar(TipoDeCambio unTipoDeCambioEditado)
    {
        TipoDeCambio? unTipoDeCambioAActualizar = Encontrar(x => x.Id == unTipoDeCambioEditado.Id);
        if (unTipoDeCambioAActualizar != null)
        {
            unTipoDeCambioAActualizar.ValorDeLaMoneda = unTipoDeCambioEditado.ValorDeLaMoneda;
        }
        _contexto.SaveChanges();
        return unTipoDeCambioAActualizar;
    }
}