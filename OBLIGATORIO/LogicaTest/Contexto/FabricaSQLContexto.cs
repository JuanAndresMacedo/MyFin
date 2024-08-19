using Memoria;
using Microsoft.EntityFrameworkCore;

namespace LogicaTest.Contexto;

public class FabricaSQLContexto
{
    public SQLContexto CrearContextoMemoria()
    {
        var optionsBuilder = new DbContextOptionsBuilder<SQLContexto>();
        optionsBuilder.UseInMemoryDatabase("TestDB");

        return new SQLContexto(optionsBuilder.Options);
    }
}