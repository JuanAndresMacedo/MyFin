using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class EspacioBDRepositorio : IRepositorio<Espacio>
{
    private SQLContexto _contexto;

    public EspacioBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }
    
    public Espacio Agregar(Espacio unEspacio)
    {
        _contexto.Espacios.Add(unEspacio);
        _contexto.SaveChanges();
        return unEspacio;
    }

    public Espacio? Encontrar(Func<Espacio, bool> filtro)
    {
        return _contexto.Espacios
            .Include(espacio => espacio.Administrador)
            .Include(espacio => espacio.Participantes)
            .Where(filtro).FirstOrDefault();
    }

    public IList<Espacio> ListarTodos()
    {
        return _contexto.Espacios
            .Include(espacio => espacio.Administrador)
            .Include(espacio => espacio.Participantes)
            .ToList();
    }

    public Espacio Eliminar(int idAEliminar)
    {
        Espacio espacioAEliminar = Encontrar(espacio => espacio.Id == idAEliminar);
        if (espacioAEliminar != null)
        {
            _contexto.Espacios.RemoveRange(espacioAEliminar);
        }
        _contexto.SaveChanges();
        return espacioAEliminar;
    }

    public Espacio? Actualizar(Espacio unEspacioEditado)
    {
        Espacio espacioAActualizar = Encontrar(x => x.Id == unEspacioEditado.Id);
        
        if (espacioAActualizar != null)
        {
            espacioAActualizar.Nombre = unEspacioEditado.Nombre;
        }
        _contexto.SaveChanges();
        return espacioAActualizar;
    }
}