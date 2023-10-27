using Dominio;
using Memoria;

namespace Logica;

public class EspacioLogica
{
    private readonly IRepositorio<Espacio> _repositorio;
    public Espacio EspacioActual { get; set; }

    public EspacioLogica(IRepositorio<Espacio> espacioRepositorio)
    {
        _repositorio = espacioRepositorio;
        EspacioActual = null;
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
        EspacioActual = null;
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
        EspacioActual = EncontrarEspacio(idEspacioActual);
    }

    public void ActualizarEspacio(Espacio unEspacioActualizado)
    {
        if (ValidarNombreAlActualizar(unEspacioActualizado.Nombre))
        {
            EspacioActual.Nombre = unEspacioActualizado.Nombre;
            _repositorio.Actualizar(unEspacioActualizado);
        }
    }

    public void AgregarNuevoParticipante(Usuario participanteAAgregar)
    {
        EspacioActual.AgregarParticipante(participanteAAgregar);
    }

    public void EliminarParticipante(Usuario participanteAEliminar)
    {
        EspacioActual.BorrarParticipante(participanteAEliminar);
    }
    
    private void ValidarNoExisteEspacio(Espacio unEspacio)
    {
        if (_repositorio.Encontrar(espacio => espacio.Nombre == unEspacio.Nombre && espacio.Administrador == unEspacio.Administrador) != null)
            throw new LogicaExcepcion("Ya existe un espacio con ese" +
                                      " nombre y usuario asociado");
    }
    
    private bool ValidarNombreAlActualizar(string nombreNuevo)
    {
        bool valido = false;
        foreach (Espacio espacio in ListarEspaciosDeUnUsuario(
                     EspacioActual.Administrador))
        {
            if (nombreNuevo == espacio.Nombre)
                throw new LogicaExcepcion("Ya tiene un " +
                                          "espacio con ese nombre");

            valido = true;
        }

        return valido;
    }
}