using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class MonetariaBDRepositorio : IRepositorio<Monetaria>
{
    private SQLContexto _contexto;

    public MonetariaBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }

    public Monetaria Agregar(Monetaria unaMonetaria)
    {
        _contexto.Monetarias.Add(unaMonetaria);
        _contexto.SaveChanges();
        return unaMonetaria;
    }

    public Monetaria? Encontrar(Func<Monetaria, bool> filtro)
    {
        return _contexto.Monetarias
            .Include(monetaria => monetaria.Espacio)
            .Include(monetaria => monetaria.Propietario)
            .Include(monetaria => monetaria.Moneda)
            .Where(filtro)
            .FirstOrDefault();
    }

    public IList<Monetaria> ListarTodos()
    {
        return _contexto.Monetarias
            .Include(monetaria => monetaria.Espacio)
            .Include(monetaria => monetaria.Propietario)
            .Include(monetaria => monetaria.Moneda)
            .ToList();
    }

    public Monetaria Eliminar(int idAEliminar)
    {
        Monetaria espacioAEliminar = Encontrar(espacio => espacio.Id == idAEliminar);
        if (espacioAEliminar != null)
        {
            _contexto.Monetarias.RemoveRange(espacioAEliminar);
        }

        _contexto.SaveChanges();
        return espacioAEliminar;
    }

    public Monetaria? Actualizar(Monetaria unaMonetariaEditada)
    {
        Monetaria? monetariaAActualizar = Encontrar(monetaria => monetaria.Id == unaMonetariaEditada.Id);

        if (monetariaAActualizar != null)
        {
            monetariaAActualizar.Nombre = unaMonetariaEditada.Nombre;
            monetariaAActualizar.Monto = unaMonetariaEditada.Monto;
            monetariaAActualizar.Moneda = unaMonetariaEditada.Moneda;
        }

        _contexto.SaveChanges();
        return monetariaAActualizar;
    }
}