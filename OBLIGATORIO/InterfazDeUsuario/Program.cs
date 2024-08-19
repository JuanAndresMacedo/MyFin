using Memoria;
using Dominio;
using Logica;
using Memoria.BaseDeDatosRepositorios;
using Memoria.SesionActual;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<SQLContexto>( options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddScoped<IRepositorio<Usuario>, UsuarioBDRepositorio>();
builder.Services.AddScoped<IRepositorio<Monetaria>, MonetariaBDRepositorio>();
builder.Services.AddScoped<IRepositorio<TarjetaDeCredito>, TarjetaDeCreditoBDRepositorio>();
builder.Services.AddScoped<IRepositorio<Espacio>, EspacioBDRepositorio>();
builder.Services.AddScoped<IRepositorio<Categoria>, CategoriaBDRepositorio>();
builder.Services.AddScoped<IRepositorio<TipoDeCambio>, TipoDeCambioBDRepositorio>();
builder.Services.AddScoped<IRepositorioObjDeGasto<ObjetivoDeGasto>, ObjetivoDeGastoBDRepositorio>();
builder.Services.AddScoped<IRepositorio<Transaccion>, TransaccionBDRepositorio>();
builder.Services.AddScoped<IRepositorioMoneda<Moneda>, MonedaBDRepositorio>();

builder.Services.AddSingleton<SesionActualMemoria>();
builder.Services.AddScoped<UsuarioLogica>();
builder.Services.AddScoped<MonetariaLogica>();
builder.Services.AddScoped<TarjetaDeCreditoLogica>();
builder.Services.AddScoped<EspacioLogica>();
builder.Services.AddScoped<CategoriaLogica>();
builder.Services.AddScoped<TipoDeCambioLogica>();
builder.Services.AddScoped<ObjetivoDeGastoLogica>();
builder.Services.AddScoped<TransaccionLogica>();
builder.Services.AddScoped<MonedaLogica>();

builder.Services.AddScoped<ReporteCategoriaLogica>();
builder.Services.AddScoped<ReporteGastosLogica>();
builder.Services.AddScoped<ReporteIngresoYEgresoLogica>();
builder.Services.AddScoped<ReporteMonetariasLogica>();
builder.Services.AddScoped<ReportesTarjetasDeCreditoLogica>();
builder.Services.AddScoped<ReporteObjetivoDeGastoLogica>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();