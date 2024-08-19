using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class TarjetaDeCreditoBDRepositorio : IRepositorio<TarjetaDeCredito>
{
    private SQLContexto _contexto;

    public TarjetaDeCreditoBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }
    
    public TarjetaDeCredito Agregar(TarjetaDeCredito unaTarjetaDeCredito)
    {
        _contexto.TarjetasDeCredito.Add(unaTarjetaDeCredito);
        _contexto.SaveChanges();
        return unaTarjetaDeCredito;
    }

    public TarjetaDeCredito? Encontrar(Func<TarjetaDeCredito, bool> filtro)
    {
        return _contexto.TarjetasDeCredito
            .Include(tarjetaDeCredito => tarjetaDeCredito.Propietario)
            .Include(tarjetaDeCredito => tarjetaDeCredito.Espacio)
            .Include(tarjetaDeCredito => tarjetaDeCredito.Moneda)
            .Where(filtro).FirstOrDefault();
    }

    public IList<TarjetaDeCredito> ListarTodos()
    {
        return _contexto.TarjetasDeCredito
            .Include(tarjetaDeCredito => tarjetaDeCredito.Propietario)
            .Include(tarjetaDeCredito => tarjetaDeCredito.Espacio)
            .Include(tarjetaDeCredito => tarjetaDeCredito.Moneda)
            .ToList();
    }

    public TarjetaDeCredito Eliminar(int idAEliminar)
    {
        TarjetaDeCredito espacioAEliminar = Encontrar(espacio => espacio.Id == idAEliminar);
        if (espacioAEliminar != null)
        {
            _contexto.TarjetasDeCredito.RemoveRange(espacioAEliminar);
        }
        _contexto.SaveChanges();
        return espacioAEliminar;
    }

    public TarjetaDeCredito? Actualizar(TarjetaDeCredito unaTarjetaDeCreditoEditada)
    {
        TarjetaDeCredito? tarjetaDeCreditoAActualizar = Encontrar(tarjetaDeCredito => tarjetaDeCredito.Id == unaTarjetaDeCreditoEditada.Id);

        if (tarjetaDeCreditoAActualizar != null)
        {
            tarjetaDeCreditoAActualizar.Nombre = unaTarjetaDeCreditoEditada.Nombre;
            tarjetaDeCreditoAActualizar.BancoEmisor = unaTarjetaDeCreditoEditada.BancoEmisor;
            tarjetaDeCreditoAActualizar.CreditoDisponible = unaTarjetaDeCreditoEditada.CreditoDisponible;
            tarjetaDeCreditoAActualizar.Moneda = unaTarjetaDeCreditoEditada.Moneda;
            tarjetaDeCreditoAActualizar.FechaDeCierre = unaTarjetaDeCreditoEditada.FechaDeCierre;
            tarjetaDeCreditoAActualizar.UltimosCuatroDigitos = unaTarjetaDeCreditoEditada.UltimosCuatroDigitos;
        }

        _contexto.SaveChanges();
        return tarjetaDeCreditoAActualizar;
    }
}