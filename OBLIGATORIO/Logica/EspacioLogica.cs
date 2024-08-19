using Dominio;
using Memoria;
using Memoria.SesionActual;

namespace Logica;

public class EspacioLogica
{
    private readonly IRepositorio<Espacio> _repositorio;
    private readonly SesionActualMemoria _sesionActual;

    public EspacioLogica(IRepositorio<Espacio> espacioRepositorio, SesionActualMemoria sesionActual)
    {
        _repositorio = espacioRepositorio;
        _sesionActual = sesionActual;
    }

    public Espacio? EspacioActual()
    {
        return _sesionActual.EspacioActual;
    }

    public Espacio AgregarEspacio(Espacio unEspacio)
    {
        ValidarNoExisteEspacio(unEspacio);
        return _repositorio.Agregar(unEspacio);
    }

    public Espacio? EncontrarEspacio(int idAEncontrar)
    {
        return _repositorio.Encontrar(espacio => espacio.Id == idAEncontrar);
    }

    public IList<Espacio> ListarEspacios()
    {
        return _repositorio.ListarTodos();
    }

    public IList<Espacio> ListarEspaciosDeUnUsuario(Usuario unUsuario)
    {
        IList<Espacio> espaciosUsuarioSeleccionado = new List<Espacio>();
        foreach (Espacio espacio in ListarEspacios())
        {
            if (espacio.Administrador.Equals(unUsuario))
            {
                espaciosUsuarioSeleccionado.Add(espacio);
            }

            if (espacio.EncontrarParticipante(unUsuario) != null)
            {
                espaciosUsuarioSeleccionado.Add(espacio);
            }
        }

        return espaciosUsuarioSeleccionado;
    }

    public Espacio? EliminarEspacio(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public void SalirDelEspacio()
    {
        _sesionActual.EspacioActual = null;
    }

    public Espacio CrearEspacioPrincipal(Usuario unUsuario)
    {
        Espacio espacioPrincipal = new Espacio()
        {
            Nombre = "Principal " + unUsuario.Nombre,
            Administrador = unUsuario
        };

        AgregarEspacio(espacioPrincipal);
        return espacioPrincipal;
    }

    public void AsignarEspacioActual(int idEspacioActual)
    {
        _sesionActual.EspacioActual = EncontrarEspacio(idEspacioActual);
    }

    public void ActualizarEspacio(Espacio unEspacioActualizado)
    {
        if (ValidarNombreAlActualizar(unEspacioActualizado.Nombre))
        {
            _sesionActual.EspacioActual.Nombre = unEspacioActualizado.Nombre;
            _repositorio.Actualizar(unEspacioActualizado);
        }
    }

    public void ActualizarParticipantesDeUnEspacio(Espacio unEspacioActualizado)
    {
        _repositorio.Actualizar(unEspacioActualizado);
    }

    public void AgregarNuevoParticipante(Usuario participanteAAgregar)
    {
        _sesionActual.EspacioActual.AgregarParticipante(participanteAAgregar);
        ActualizarParticipantesDeUnEspacio(_sesionActual.EspacioActual);
    }

    public void EliminarParticipante(Usuario participanteAEliminar)
    {
        _sesionActual.EspacioActual.EliminarParticipante(participanteAEliminar);
        ActualizarParticipantesDeUnEspacio(_sesionActual.EspacioActual);
    }

    private void ValidarNoExisteEspacio(Espacio unEspacio)
    {
        if (_repositorio.Encontrar(espacio => espacio.Nombre == unEspacio.Nombre &&
                                              espacio.Administrador.Equals(unEspacio.Administrador)) != null)
            throw new LogicaExcepcion("Ya existe un espacio con ese" +
                                      " nombre y usuario asociado");
    }

    private bool ValidarNombreAlActualizar(string nombreNuevo)
    {
        bool valido = false;
        foreach (Espacio espacio in ListarEspaciosDeUnUsuario(
                     _sesionActual.EspacioActual.Administrador))
        {
            if (nombreNuevo == espacio.Nombre)
                throw new LogicaExcepcion("Ya tiene un " +
                                          "espacio con ese nombre");

            valido = true;
        }

        return valido;
    }
}