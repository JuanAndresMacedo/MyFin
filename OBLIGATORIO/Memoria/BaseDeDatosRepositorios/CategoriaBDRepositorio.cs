using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class CategoriaBDRepositorio : IRepositorio<Categoria>
{
    private SQLContexto _contexto;

    public CategoriaBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }

    public Categoria Agregar(Categoria unaCategoria)
    {
        _contexto.Categorias.Add(unaCategoria);
        _contexto.SaveChanges();
        return unaCategoria;
    }

    public Categoria? Encontrar(Func<Categoria, bool> filtro)
    {
        return _contexto.Categorias
            .Include(categoria => categoria.Espacio)
            .Include(categoria => categoria.UsuarioCreador)
            .Include(categoria =>categoria.ObjetivosDeGasto)
            .Where(filtro).FirstOrDefault();
    }

    public IList<Categoria> ListarTodos()
    {
        return _contexto.Categorias
            .Include(categoria => categoria.Espacio)
            .Include(categoria => categoria.UsuarioCreador)
            .Include(categoria =>categoria.ObjetivosDeGasto)
            .ToList();
    }

    public Categoria Eliminar(int idAEliminar)
    {
        Categoria categoriaAEliminar = Encontrar(categoria => categoria.Id == idAEliminar);
        if (categoriaAEliminar != null)
        {
            _contexto.Categorias.RemoveRange(categoriaAEliminar);
        }
        _contexto.SaveChanges();
        return categoriaAEliminar;
    }

    public Categoria? Actualizar(Categoria unaCategoriaEditada)
    {
        Categoria categoriaAActualizar = Encontrar(x => x.Id == unaCategoriaEditada.Id);
        
        if (categoriaAActualizar != null)
        {
            categoriaAActualizar.Estatus = unaCategoriaEditada.Estatus;
            categoriaAActualizar.Tipo = unaCategoriaEditada.Tipo;
        }
        _contexto.SaveChanges();
        return categoriaAActualizar;
    }
}
    