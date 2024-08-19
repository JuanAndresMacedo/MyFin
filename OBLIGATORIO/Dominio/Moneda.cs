namespace Dominio;

public class Moneda
{
    public int Id { get; set; }
    
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
    
    private string _simboloMonetario;

    public string? SimboloMonetario
    {
        get => _simboloMonetario;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El símbolo monetario no puede ser vacío.");
            
            _simboloMonetario = value;
        }
    }
    
    public override bool Equals(object? moneda)
    {
        Moneda unaMoneda = (Moneda)moneda;
        return unaMoneda.Id == Id;
    }
}