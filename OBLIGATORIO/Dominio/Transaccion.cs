using Dominio.Constantes;

namespace Dominio;

public class Transaccion
{
    public int Id { get; set; }

    public Espacio Espacio { get; set; }
    public DateTime Fecha { get; set; }

    private string _tipo;

    public string Tipo
    {
        get => _tipo;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El tipo no puede ser vacío.");

            if (value != ConstantesCategoria.tipoCosto && value != ConstantesCategoria.tipoIngreso)
                throw new DominioExcepcion("El tipo solo puede ser " +
                                           "Costo o Ingreso.");

            _tipo = value;
        }
    }

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

    private float _monto;

    public float? Monto
    {
        get => _monto;
        set
        {
            if (value <= 0 || value is null)
                throw new DominioExcepcion("El monto debe ser mayor a 0.");

            _monto = (float)value;
        }
    }

    private Moneda _moneda;
    public Moneda? Moneda
    {
        get => _moneda;
        set
        {
            if (value is null)
                throw new DominioExcepcion("La moneda de la transacción " +
                                           "no puede ser vacía.");

            _moneda = value;
        }
    }

    private Cuenta _cuenta;

    public Cuenta? Cuenta
    {
        get => _cuenta;
        set
        {
            if (value is null)
                throw new DominioExcepcion("La transacción debe tener una" +
                                           " cuenta asociada.");

            _cuenta = value;
        }
    }

    private Categoria _categoria;

    public Categoria? Categoria
    {
        get => _categoria;
        set
        {
            if (value is null)
                throw new DominioExcepcion("La transacción debe contener " +
                                           "una categoría.");

            if (value.Estatus == ConstantesCategoria.estatusInactiva)
                throw new DominioExcepcion("La transacción debe crearse" +
                                           " con una categoria activa.");

            _categoria = value;
        }
    }

    public override bool Equals(object? transaccion)
    {
        Transaccion unaTransaccion = (Transaccion)transaccion;
        return unaTransaccion.Id == Id;
    }
}