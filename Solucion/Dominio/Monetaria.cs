namespace Dominio;

public class Monetaria : Cuenta
{
    private float _monto;

    public float? Monto
    {
        get => _monto;
        set
        {
            if (value is null || value < 0f)
                throw new DominioExcepcion("El monto de la cuenta monetaria " +
                                           "no es suficiente (No puede ser negativo).");

            _monto = (float)value;
        }
    }

    public override string DevolverTipoDeCuenta()
    {
        return "monetaria";
    }

    public override void ActualizarCuenta(Cuenta unaCuentaActualizada)
    {
        if (unaCuentaActualizada != null)
        {
            Monto = ((Monetaria)unaCuentaActualizada).Monto;
            Moneda = unaCuentaActualizada.Moneda;
            Nombre = unaCuentaActualizada.Nombre;
        }
    }

    public override void DescontarCosto(float? cantidadDeMonto)
    {
        Monto -= cantidadDeMonto;
    }

    public override void SumarIngreso(float? cantidadDeMonto)
    {
        Monto += cantidadDeMonto;
    }
}