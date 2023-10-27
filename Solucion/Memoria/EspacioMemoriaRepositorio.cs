using Dominio;

namespace Memoria;

public class EspacioMemoriaRepositorio : IRepositorio<Espacio>
{
    
    private List<Espacio> _espacios = new List<Espacio>();
    
    public Espacio Agregar(Espacio unEspacio)
    {
        unEspacio.Id = _espacios.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _espacios.Add(unEspacio);
        return unEspacio;
    }

    public Espacio? Encontrar(Func<Espacio, bool> filtro)
    {
        return _espacios.Where(filtro).FirstOrDefault();
    }

    public IList<Espacio> ListarTodos()
    {
        return this._espacios;
    }

    public Espacio Eliminar(int idAEliminar)
    {
        bool seElimino = false;
        Espacio? espacio = null;
        for (int i=0; i<_espacios.Count && !seElimino;i++)
        {
            if (_espacios[i].Id == idAEliminar)
            {
                espacio = _espacios[i];
                _espacios.Remove(_espacios[i]);
                seElimino = true;
            }
        }

        return espacio;
    }

    public Espacio? Actualizar(Espacio unEspacioEditado)
    {
        Espacio espacioAActualizar = Encontrar(x => x.Id == unEspacioEditado.Id);
        if (espacioAActualizar != null)
        {
            espacioAActualizar.Nombre = unEspacioEditado.Nombre;
        }

        return espacioAActualizar;
    }
    
}