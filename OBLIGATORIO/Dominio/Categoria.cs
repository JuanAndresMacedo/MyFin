using Dominio.Constantes;

namespace Dominio;

public class Categoria
{
    public int Id { get; set; }
    public Usuario UsuarioCreador { get; set; }
    public Espacio Espacio { get; set; }
    public DateTime FechaDeCreacion { get; set; }
    
    public List<ObjetivoDeGasto> ObjetivosDeGasto { get; set; } = new List<ObjetivoDeGasto>();

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
            if (value != ConstantesCategoria.tipoCosto && 
                value != ConstantesCategoria.tipoIngreso)
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
            if (value != ConstantesCategoria.estatusActiva &&
                value != ConstantesCategoria.estatusInactiva)
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