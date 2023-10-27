using Dominio;

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
        return _contexto.Usuarios.Where(filtro).FirstOrDefault();
    }
    
    public IList<Usuario> ListarTodos()
    {
        return _contexto.Usuarios.ToList();
    }
    
    public Usuario Eliminar(int id)
    {
        bool seElimino = false;
        Usuario? usuario = null;
        for (int i=0; i< _contexto.Usuarios.Count() && !seElimino;i++)
        {
            if (_contexto.Usuarios.ToList()[i].Id == id)
            {
                usuario = _contexto.Usuarios.ToList()[i];
                _contexto.Usuarios.ToList().Remove(_contexto.Usuarios.ToList()[i]);
                seElimino = true;
                _contexto.SaveChanges();
            }
        }

        return usuario;
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