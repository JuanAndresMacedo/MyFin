using Dominio;

namespace Memoria.BaseDeDatosRepositorios;

public class CategoriaBDRepositorio : IRepositorio<Categoria>
{
    private List<Categoria> _categorias = new List<Categoria>();

    public Categoria Agregar(Categoria unaCategoria)
    {
        unaCategoria.Id = _categorias.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _categorias.Add(unaCategoria);
        return unaCategoria;
    }

    public Categoria? Encontrar(Func<Categoria, bool> filtro)
    {
        return _categorias.Where(filtro).FirstOrDefault();
    }

    public IList<Categoria> ListarTodos()
    {
        return _categorias;
    }

    public Categoria Eliminar(int idAEliminar)
    {
        bool seElimino = false;
        Categoria? categoria = null;
        for (int i = 0; i < _categorias.Count && !seElimino; i++)
        {
            if (_categorias[i].Id == idAEliminar)
            {
                categoria = _categorias[i];
                _categorias.Remove(_categorias[i]);
                seElimino = true;
            }
        }

        return categoria;
    }

    public Categoria? Actualizar(Categoria unaCategoriaEditada)
    {
        Categoria categoriaAActualizar = Encontrar(x => x.Id == unaCategoriaEditada.Id);

        if (categoriaAActualizar != null)
        {
            categoriaAActualizar.Estatus = unaCategoriaEditada.Estatus;
            categoriaAActualizar.Tipo = unaCategoriaEditada.Tipo;
        }

        return categoriaAActualizar;
    }
}
    