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

    private string _moneda;

    public string? Moneda
    {
        get => _moneda;
        set
        {
            if (value != "UYU" && value != "US$")
                throw new DominioExcepcion("La moneda de la cuenta debe ser " +
                                           "Pesos uruguayos o Dolares.");

            _moneda = value;
        }
    }

    public abstract string DevolverTipoDeCuenta();

    public abstract void ActualizarCuenta(Cuenta unaCuentaActualizada);

    public abstract void DescontarCosto(float? cantidadDePlata);

    public abstract void SumarIngreso(float? cantidadDePlata);

    public override bool Equals(object? cuenta)
    {
        Cuenta unaCuenta = (Cuenta)cuenta;
        return unaCuenta.Id == Id;
    }
}