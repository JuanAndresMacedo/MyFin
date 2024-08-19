namespace Memoria;

public interface IRepositorioMoneda <T>
{
    T Agregar(T unElemento);

    T? Encontrar(Func<T, bool> filtro);

    IList<T> ListarTodos();
}