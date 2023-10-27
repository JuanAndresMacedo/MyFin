namespace Memoria;

public interface IRepositorio <T>
{
    T Agregar(T unElemento);

    T? Encontrar(Func<T, bool> filtro);

    IList<T> ListarTodos();

    T? Eliminar(int id);
    
    T? Actualizar(T unElemento);
}