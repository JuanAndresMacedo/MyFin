using Dominio;

namespace Memoria;

public class CuentaMemoriaRepositorio : IRepositorio<Cuenta>
{
    private List<Cuenta> _cuentas = new List<Cuenta>();

    public Cuenta Agregar(Cuenta unaCuenta)
    {
        unaCuenta.Id = _cuentas.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _cuentas.Add(unaCuenta);
        return unaCuenta;
    }

    public Cuenta? Encontrar(Func<Cuenta, bool> filtro)
    {
        return _cuentas.Where(filtro).FirstOrDefault();
    }

    public IList<Cuenta> ListarTodos()
    {
        return _cuentas;
    }

    public Cuenta Eliminar(int idAEliminar)
    {
        bool seElimino = false;
        Cuenta? cuenta = null;
        for (int i = 0; i < _cuentas.Count && !seElimino; i++)
        {
            if (_cuentas[i].Id == idAEliminar)
            {
                cuenta = _cuentas[i];
                _cuentas.Remove(_cuentas[i]);
                seElimino = true;
            }
        }

        return cuenta;
    }

    public Cuenta? Actualizar(Cuenta unaCuentaEditada)
    {
        Cuenta cuentaAActualizar = Encontrar(x => x.Id == unaCuentaEditada.Id);

        if (cuentaAActualizar != null)
        {
            cuentaAActualizar.ActualizarCuenta(unaCuentaEditada);
        }

        return cuentaAActualizar;
    }
}