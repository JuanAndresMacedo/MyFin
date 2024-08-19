namespace Dominio;

public abstract class Cuenta
{
    public int Id { get; set; }
    public Usuario Propietario { get; set; }
    public Espacio Espacio { get; set; }
    public DateTime FechaDeCreacion { get; set; }

    private string _nombre;

    public string? Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El nombre de la cuenta " +
                                           "no puede ser vacío.");

            _nombre = value;
        }
    }
    
    private Moneda _moneda;
    public Moneda? Moneda
    {
        get => _moneda;
        set
        {
            if (value is null)
                throw new DominioExcepcion("La moneda de la cuenta " +
                                           "no puede ser vacía.");

            _moneda = value;
        }
    }
    
    public abstract float? DevolverDineroCuenta();
    
    public abstract void AsignarDineroCuenta(float? dinero);

    public override bool Equals(object? cuenta)
    {
        Cuenta unaCuenta = (Cuenta)cuenta;
        return unaCuenta.Id == Id;
    }
}