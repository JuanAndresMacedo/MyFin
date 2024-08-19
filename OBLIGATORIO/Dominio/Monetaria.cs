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

    public override float? DevolverDineroCuenta()
    {
        return Monto;
    }

    public override void AsignarDineroCuenta(float? dinero)
    {
        Monto = dinero;
    }
}