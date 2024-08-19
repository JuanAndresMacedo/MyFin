namespace Dominio;

public class ObjetivoDeGasto
{
    public int Id { get; set; }
    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    public Usuario UsuarioCreador { get; set; }

    public Espacio Espacio { get; set; }

    private string _titulo;

    public string? Titulo
    {
        get => _titulo;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El título no puede ser vacío.");

            _titulo = value;
        }
    }

    private float _montoMaximo;

    public float MontoMaximo
    {
        get => _montoMaximo;
        set
        {
            if (value <= 0)
                throw new DominioExcepcion("El monto máximo debe ser " +
                                           "mayor a 0.");

            _montoMaximo = value;
        }
    }
    
    public string? Token { get; set; }

    public void AgregarCategoria(Categoria unaCategoria)
    {
        Categorias.Add(unaCategoria);
    }

    public override bool Equals(object? objetivoDeGasto)
    {
        ObjetivoDeGasto unObjetivoDeGasto = (ObjetivoDeGasto)objetivoDeGasto;

        return unObjetivoDeGasto.Id == Id;
    }
}