using System.Runtime.CompilerServices;
using Dominio;
using Memoria;

namespace Logica;

public class UsuarioLogica
{
    private readonly IRepositorio<Usuario> _repositorio;
    public Usuario UsuarioActual { get; set; }

    public UsuarioLogica(IRepositorio<Usuario> usuarioRepositorio)
    {
        _repositorio = usuarioRepositorio;
        UsuarioActual = null;
    }

    public Usuario AgregarUsuario(Usuario unUsuario)
    {
        return _repositorio.Agregar(unUsuario);
    }

    public Usuario? EncontrarUsuario(int idAEncontrar)
    {
        return _repositorio.Encontrar(x => x.Id == idAEncontrar);
    }

    public IList<Usuario> ListarUsuarios()
    {
        return _repositorio.ListarTodos();
    }

    public Usuario? EliminarUsuario(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public Usuario? ActualizarUsuario(Usuario unUsuarioActualizado)
    {
        return _repositorio.Actualizar(unUsuarioActualizado);
    }

    public void IniciarSesion(Usuario usuarioIniciando)
    {
        ValidarEstaRegistrado(usuarioIniciando);
        ValidarContrasenaCorrecto(usuarioIniciando);
        UsuarioActual = _repositorio.Encontrar(usuario => usuario.Correo == usuarioIniciando.Correo);
    }

    public void CerrarSesion()
    {
        UsuarioActual = null;
    }

    public void Registrarse(Usuario usuarioRegistrandose,
        string confirmacionContrasena)
    {
        ValidarConfirmacionContrasena(usuarioRegistrandose,
            confirmacionContrasena);
        ValidarCorreoUnico(usuarioRegistrandose);
        AgregarUsuario(usuarioRegistrandose);
    }

    public void PermitirCambiarContraseña(Usuario unUsuario, string contrasenaActual)
    {
        if (unUsuario.Contrasena != contrasenaActual)
            throw new LogicaExcepcion("Para cambiar la contraseña debe colocar " +
                                      "la contraseña actual correctamente.");
    }

    public void ValidarConfirmacionContrasena(Usuario unUsuario, string contrasenaAConfirmar)
    {
        if (unUsuario.Contrasena != contrasenaAConfirmar)
            throw new LogicaExcepcion("La contraseñas no coinciden, " +
                                      "confirme su contraseña correctamente.");
    }

    private void ValidarCorreoUnico(Usuario unUsuario)
    {
        if (_repositorio.Encontrar(usuario => usuario.Correo == unUsuario.Correo) != null)
            throw new LogicaExcepcion("Un usuario con este " +
                                      "correo ya esta registrado.");
    }
    
    private void ValidarContrasenaCorrecto(Usuario unUsuario)
    {
        if (_repositorio.Encontrar(usuario => usuario.Correo == unUsuario.Correo).Contrasena != unUsuario.Contrasena)
            throw new LogicaExcepcion("Contraseña incorrecta.");
    }
    
    private void ValidarEstaRegistrado(Usuario unUsuario)
    {
        if (_repositorio.Encontrar(usuario => usuario.Correo == unUsuario.Correo) == null)
            throw new LogicaExcepcion("No existe un usuario asociado " +
                                      "a este correo.");
    }
}