using Dominio;

namespace Memoria;

public class UsuarioMemoriaRepositorio : IRepositorio<Usuario>
{
    private List<Usuario> _usuarios = new List<Usuario>();
    public Usuario Agregar(Usuario unUsuario)
    {
        unUsuario.Id = _usuarios.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _usuarios.Add(unUsuario);
        return unUsuario;
    }

    public Usuario? Encontrar(Func<Usuario, bool> filtro)
    {
            return _usuarios.Where(filtro).FirstOrDefault();
    }
    
    public IList<Usuario> ListarTodos()
    {
        return this._usuarios;
    }
    
    public Usuario Eliminar(int id)
    {
        bool seElimino = false;
        Usuario? usuario = null;
        for (int i=0; i<_usuarios.Count && !seElimino;i++)
        {
            if (_usuarios[i].Id == id)
            {
                usuario = _usuarios[i];
                _usuarios.Remove(_usuarios[i]);
                seElimino = true;
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
        return unUsuarioAActualizar;
    }
}
