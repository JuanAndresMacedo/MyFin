namespace Dominio;

public class TarjetaDeCredito : Cuenta
{
    public DateTime FechaDeCierre { get; set; }

    private string _bancoEmisor;

    public string? BancoEmisor
    {
        get => _bancoEmisor;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El banco emisor " +
                                           "no puede ser vacío.");

            _bancoEmisor = value;
        }
    }

    private string _ultimosCuatroDigitos;

    public string? UltimosCuatroDigitos
    {
        get => _ultimosCuatroDigitos;
        set
        {
            if (value is null || value.Length != 4)
                throw new DominioExcepcion("Los ultimos cuatro digitos " +
                                           "deben ser cuatro digitos numéricos.");

            bool soloContieneNumeros = true;

            for (int i = 0; i < value.Length && soloContieneNumeros; i++)
            {
                if (!char.IsDigit(value[i]))
                    soloContieneNumeros = false;
            }

            if (!soloContieneNumeros)
                throw new DominioExcepcion("Los ultimos cuatro digitos " +
                                           "deben ser números.");

            _ultimosCuatroDigitos = value;
        }
    }

    private float _creditoDisponible;

    public float? CreditoDisponible
    {
        get => _creditoDisponible;
        set
        {
            if (value is null || value < 0f)
                throw new DominioExcepcion("El credito de la tarjeta no es " +
                                           "suficiente (No puede ser negativo).");

            _creditoDisponible = (float)value;
        }
    }

    public override string DevolverTipoDeCuenta()
    {
        return "tarjetaDeCredito";
    }

    public override void ActualizarCuenta(Cuenta unaCuentaActualizada)
    {
        if (unaCuentaActualizada != null)
        {
            Nombre = ((TarjetaDeCredito)unaCuentaActualizada).Nombre;
            BancoEmisor = ((TarjetaDeCredito)unaCuentaActualizada).BancoEmisor;
            FechaDeCierre = ((TarjetaDeCredito)unaCuentaActualizada).FechaDeCierre;
            CreditoDisponible = ((TarjetaDeCredito)unaCuentaActualizada).CreditoDisponible;
            UltimosCuatroDigitos = ((TarjetaDeCredito)unaCuentaActualizada).UltimosCuatroDigitos;
            Moneda = unaCuentaActualizada.Moneda;
        }
    }

    public override void DescontarCosto(float? cantidadDeCredito)
    {
        CreditoDisponible -= cantidadDeCredito;
    }

    public override void SumarIngreso(float? cantidadDeCredito)
    {
        CreditoDisponible += cantidadDeCredito;
    }
}