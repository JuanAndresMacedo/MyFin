using Dominio;

namespace Memoria;

public interface IRepositorioObjDeGasto <T>
{
    T Agregar(T unElemento);

    T? Encontrar(Func<T, bool> filtro);

    IList<T> ListarTodos();
    
    T? Actualizar(T unElemento);
}