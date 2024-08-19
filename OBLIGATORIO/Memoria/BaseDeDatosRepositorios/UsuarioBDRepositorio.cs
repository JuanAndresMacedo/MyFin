using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class UsuarioBDRepositorio : IRepositorio<Usuario>
{
    private SQLContexto _contexto;
    public UsuarioBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }
    public Usuario Agregar(Usuario unUsuario)
    {
        _contexto.Usuarios.Add(unUsuario);
        _contexto.SaveChanges();
        return unUsuario;
    }

    public Usuario? Encontrar(Func<Usuario, bool> filtro)
    {
        return _contexto.Usuarios
            .Include(usuario => usuario.Espacios)
            .Include(usuario => usuario.EspaciosQueAdministra)
            .Where(filtro).FirstOrDefault();
    }
    
    public IList<Usuario> ListarTodos()
    {
        return _contexto.Usuarios
            .Include(usuario => usuario.Espacios)
            .Include(usuario => usuario.EspaciosQueAdministra)
            .ToList();
    }
    
    public Usuario Eliminar(int id)
    {
        Usuario UsuarioAEliminar = Encontrar(usuario => usuario.Id == id);
        if (UsuarioAEliminar != null)
        {
            _contexto.Usuarios.RemoveRange(UsuarioAEliminar);
        }
        _contexto.SaveChanges();
        return UsuarioAEliminar;
    }

    public Usuario? Actualizar(Usuario unUsuarioEditado)
    {
        Usuario? unUsuarioAActualizar = Encontrar(x => x.Id == unUsuarioEditado.Id);
        
        if (unUsuarioAActualizar != null)
        {
            unUsuarioAActualizar.Nombre = unUsuarioEditado.Nombre;
            unUsuarioAActualizar.Apellido = unUsuarioEditado.Apellido;
            unUsuarioAActualizar.Contrasena = unUsuarioEditado.Contrasena;
            unUsuarioAActualizar.Direccion = unUsuarioEditado.Direccion;
            
        }
        _contexto.SaveChanges();
        return unUsuarioAActualizar;
    }
}