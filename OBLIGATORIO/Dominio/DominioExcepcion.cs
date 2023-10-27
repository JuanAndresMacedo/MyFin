namespace Dominio;

public class DominioExcepcion : Exception
{
    public DominioExcepcion(string? mensaje) : base(mensaje)
    {
    }
}