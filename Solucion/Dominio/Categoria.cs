namespace Dominio;

public class Categoria
{
    public int Id { get; set; }
    public Usuario UsuarioCreador { get; set; }
    public Espacio Espacio { get; set; }
    public DateTime FechaDeCreacion { get; set; }

    private string _nombre;

    public string? Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El nombre de la categoría" +
                                           " no puede ser vacío.");

            _nombre = value;
        }
    }

    private string _tipo;

    public string? Tipo
    {
        get => _tipo;
        set
        {
            if (value != "Costo" && value != "Ingreso")
                throw new DominioExcepcion("El tipo debe ser " +
                                           "Costo o Ingreso.");

            _tipo = value;
        }
    }

    private string _estatus;

    public string? Estatus
    {
        get => _estatus;
        set
        {
            if (value != "Activa" && value != "Inactiva")
                throw new DominioExcepcion("El estatus debe ser " +
                                           "Activa o Inactiva.");

            _estatus = value;
        }
    }

    public override bool Equals(object? categoria)
    {
        Categoria unaCategoria = (Categoria)categoria;
        return unaCategoria.Id == Id;
    }
}