namespace Dominio;

public class Espacio
{
    public int Id { get; set; }

    public List<Usuario> Participantes { get; set; } = new List<Usuario>();

    private string _nombre;

    public string? Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El nombre no puede ser vacío.");

            _nombre = value;
        }
    }

    public int AdministradorId { get; set; }
    
    private Usuario _administrador;

    public Usuario? Administrador
    {
        get => _administrador;
        set
        {
            if (value is null)
                throw new DominioExcepcion("Debe haber un administrador " +
                                           "en el espacio.");

            _administrador = value;
        }
    }

    public void AgregarParticipante(Usuario? unParticipante)
    {
        if (unParticipante is null)
            throw new DominioExcepcion("El nuevo participante no esta " +
                                       "registrado en el sitio.");

        ValidarParticipante(unParticipante);
        Participantes.Add(unParticipante);
    }

    public Usuario? EncontrarParticipante(Usuario unParticipante)
    {
        Usuario? nuevoParticipante = null;
        
            foreach (Usuario participante in Participantes)
            {
                if (unParticipante.Equals(participante))
                    nuevoParticipante = participante;
            }

        return nuevoParticipante;
    }

    public void EliminarParticipante(Usuario unParticipante)
    {
        if (EncontrarParticipante(unParticipante) == null)
            throw new DominioExcepcion("El participante a eliminar no esta " +
                                       "en este espacio.");

        bool seElimino = false;
        for (int i = 0; i < Participantes.Count && !seElimino; i++)
        {
            if (Participantes[i].Equals(unParticipante))
            {
                Participantes.Remove(Participantes[i]);
                seElimino = true;
            }
        }
    }

    public override bool Equals(object? espacio)
    {
        Espacio unEspacio = (Espacio)espacio;
        return unEspacio.Id == Id;
    }

    private void ValidarParticipante(Usuario unParticipante)
    {
        if (Administrador.Equals(unParticipante))
            throw new DominioExcepcion("Este usuario ya es un participante " +
                                       "de este espacio (es el administrador).");
        
        foreach (Usuario participante in Participantes)
        {
            if (participante.Equals(unParticipante))
                throw new DominioExcepcion("Este usuario ya es un participante" +
                                           " de este espacio.");
        }
    }
}