namespace Dominio;

public class TipoDeCambio
{

    public int Id { get; set; }
    public Usuario UsuarioCreador { get; set; }
    public Espacio Espacio { get; set; }
    public DateTime Fecha { get; set; }

    private float _valorDelDolar;

    public float? ValorDelDolar
    {
        get => _valorDelDolar;
        set
        {
            if (value is null || value <= 0f)
                throw new DominioExcepcion("El valor del dolar no puede" +
                                           " ser vacío/negativo/cero");

            _valorDelDolar = (float)value;
        }
    }

    public override bool Equals(object? tipoDeCambio)
    {
        TipoDeCambio tipoDC = (TipoDeCambio)tipoDeCambio;
        return tipoDC.Id == Id;
    }
}