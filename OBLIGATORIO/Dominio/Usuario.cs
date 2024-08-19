namespace Dominio;

public class Usuario
{
    public int Id { get; set; }
    
    public List<Espacio> Espacios { get; set; } = new List<Espacio>();
    public List<Espacio> EspaciosQueAdministra { get; set; } = new List<Espacio>();
    
    private string _correo;

    public string? Correo
    {
        get => _correo;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El correo no puede ser vacío.");

            if (!value.Contains('@') || (!value.EndsWith(".com")))
                throw new DominioExcepcion("El correo debe contener un @ y " +
                                           "finalizar en .com.");

            string[] textoIzquierdaDerechaArroba = value.Split('@');
            string textoIzquierda = textoIzquierdaDerechaArroba[0];
            string textoDerecha = textoIzquierdaDerechaArroba[1];

            if (textoIzquierda.Length < 1 || textoDerecha.Length < 1 
                                          || textoDerecha == ".com")
                throw new DominioExcepcion("El correo debe tener al menos" +
                                           " un caracter a la izquierda y " +
                                           "a la derecha del arroba.");
            
            _correo = value;
        }
    }

    private string _contrasena;

    public string? Contrasena
    {
        get => _contrasena;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("La contraseña no puede ser vacía.");

            if (value.Length < 10 || value.Length > 30)
                throw new DominioExcepcion("La contraseña debe tener un mínimo " +
                                           "de 10 y un máximo de 30 caracteres.");

            if (!value.Any(char.IsUpper))
                throw new DominioExcepcion("La contraseña debe tener como" +
                                           " mínimo una mayúscula.");

            _contrasena = value;
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

    private string _apellido;

    public string? Apellido
    {
        get => _apellido;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DominioExcepcion("El apellido no puede ser vacío.");

            _apellido = value;
        }
    }

    private string _direccion;

    public string? Direccion
    {
        get => _direccion;
        set
        {
            if (value is null)
            {
                _direccion = "";
            }
            else
            {
                _direccion = value;
            }
        }
    }

    public override bool Equals(object? usuario)
    {
        Usuario unUsuario = (Usuario)usuario;
        return unUsuario.Id == Id;
    }
}