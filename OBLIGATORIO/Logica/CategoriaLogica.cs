using Dominio;
using Dominio.Constantes;
using Memoria;

namespace Logica;

public class CategoriaLogica
{
    private readonly IRepositorio<Categoria> _repositorio;

    public CategoriaLogica(IRepositorio<Categoria> categoriaRepositorio)
    {
        _repositorio = categoriaRepositorio;
    }

    public Categoria AgregarCategoria(Categoria unaCategoria)
    {
        ValidarCategoriaNombreUnico(unaCategoria);
        return _repositorio.Agregar(unaCategoria);
    }

    public Categoria? EncontrarCategoria(int idAEncontrar)
    {
        return _repositorio.Encontrar(categoria => categoria.Id == idAEncontrar);
    }

    public IList<Categoria> ListarCategorias()
    {
        return _repositorio.ListarTodos();
    }

    public List<Categoria> ListarCategoriasDeUnEspacio(Espacio unEspacio)
    {
        List<Categoria> categorias = new List<Categoria>();
        foreach (Categoria categoria in _repositorio.ListarTodos())
        {
            if (categoria.Espacio.Equals(unEspacio))
            {
                categorias.Add(categoria);
            }
        }

        return categorias;
    }

    public List<Categoria> ListarCategoriasActivasDeUnEspacioPorTipo(
        Espacio unEspacio, string unTipo)
    {
        List<Categoria> categorias = new List<Categoria>();
        foreach (Categoria categoria in _repositorio.ListarTodos())
        {
            if (categoria.Espacio.Equals(unEspacio) &&
                categoria.Estatus == ConstantesCategoria.estatusActiva &&
                categoria.Tipo == unTipo)
            {
                categorias.Add(categoria);
            }
        }

        return categorias;
    }

    public Categoria? EliminarCategoria(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public Categoria? ActualizarCategoria(Categoria unaCategoriaActualizada)
    {
        return _repositorio.Actualizar(unaCategoriaActualizada);
    }

    public bool ValidarSiCategoriaTieneUnaTransacciónAsociada(
        Categoria unaCategoria, Espacio unEspacio, TransaccionLogica transaccionLogica)
    {
        bool tieneTransaccionAsociada = false;

        List<Transaccion> transacciones =
            transaccionLogica.ListarTransaccionesDeUnEspacio(unEspacio).ToList();

        for (int i = 0; i < transacciones.Count && !tieneTransaccionAsociada; i++)
        {
            if (transacciones[i].Categoria.Equals(unaCategoria))
            {
                tieneTransaccionAsociada = true;
            }
        }

        return tieneTransaccionAsociada;
    }

    private void ValidarCategoriaNombreUnico(Categoria unaCategoria)
    {
        if (_repositorio.Encontrar(categoria => categoria.Nombre == unaCategoria.Nombre &&
                                                categoria.Espacio.Equals(unaCategoria.Espacio)) != null)
            throw new LogicaExcepcion("No es posible agregar dos categorias " +
                                      "con el mismo nombre");
    }
}