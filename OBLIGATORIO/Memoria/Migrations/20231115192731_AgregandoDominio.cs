using System;
using Dominio.Constantes;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memoria.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoDominio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimboloMonetario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Espacios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministradorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Espacios_Usuarios_AdministradorId",
                        column: x => x.AdministradorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categorias_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropietarioId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonedaId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: true),
                    FechaDeCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BancoEmisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimosCuatroDigitos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditoDisponible = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuenta_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cuenta_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cuenta_Usuarios_PropietarioId",
                        column: x => x.PropietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EspacioUsuario",
                columns: table => new
                {
                    EspaciosId = table.Column<int>(type: "int", nullable: false),
                    ParticipantesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspacioUsuario", x => new { x.EspaciosId, x.ParticipantesId });
                    table.ForeignKey(
                        name: "FK_EspacioUsuario_Espacios_EspaciosId",
                        column: x => x.EspaciosId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspacioUsuario_Usuarios_ParticipantesId",
                        column: x => x.ParticipantesId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ObjetivoDeGastos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoMaximo = table.Column<float>(type: "real", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetivoDeGastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjetivoDeGastos_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjetivoDeGastos_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TiposDeCambios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorDeLaMoneda = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeCambios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposDeCambios_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TiposDeCambios_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TiposDeCambios_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<float>(type: "real", nullable: true),
                    MonedaId = table.Column<int>(type: "int", nullable: true),
                    CuentaId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacciones_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacciones_Cuenta_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "Cuenta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacciones_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoriaObjetivoDeGasto",
                columns: table => new
                {
                    CategoriasId = table.Column<int>(type: "int", nullable: false),
                    ObjetivosDeGastoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaObjetivoDeGasto", x => new { x.CategoriasId, x.ObjetivosDeGastoId });
                    table.ForeignKey(
                        name: "FK_CategoriaObjetivoDeGasto_Categorias_CategoriasId",
                        column: x => x.CategoriasId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaObjetivoDeGasto_ObjetivoDeGastos_ObjetivosDeGastoId",
                        column: x => x.ObjetivosDeGastoId,
                        principalTable: "ObjetivoDeGastos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaObjetivoDeGasto_ObjetivosDeGastoId",
                table: "CategoriaObjetivoDeGasto",
                column: "ObjetivosDeGastoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EspacioId",
                table: "Categorias",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_UsuarioCreadorId",
                table: "Categorias",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_EspacioId",
                table: "Cuenta",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_MonedaId",
                table: "Cuenta",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_PropietarioId",
                table: "Cuenta",
                column: "PropietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_AdministradorId",
                table: "Espacios",
                column: "AdministradorId");

            migrationBuilder.CreateIndex(
                name: "IX_EspacioUsuario_ParticipantesId",
                table: "EspacioUsuario",
                column: "ParticipantesId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoDeGastos_EspacioId",
                table: "ObjetivoDeGastos",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoDeGastos_UsuarioCreadorId",
                table: "ObjetivoDeGastos",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDeCambios_EspacioId",
                table: "TiposDeCambios",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDeCambios_MonedaId",
                table: "TiposDeCambios",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDeCambios_UsuarioCreadorId",
                table: "TiposDeCambios",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CategoriaId",
                table: "Transacciones",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaId",
                table: "Transacciones",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_EspacioId",
                table: "Transacciones",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_MonedaId",
                table: "Transacciones",
                column: "MonedaId");
            
            migrationBuilder.InsertData(
                table: "Monedas",
                columns: new[] { "Nombre", "SimboloMonetario" },
                values: new object[] { ConstantesMoneda.PesoUruguayo, "UYU" });


            migrationBuilder.InsertData(
                table: "Monedas",
                columns: new[] { "Nombre", "SimboloMonetario" },
                values: new object[] { ConstantesMoneda.Dolar, "USD" });


            migrationBuilder.InsertData(
                table: "Monedas",
                columns: new[] { "Nombre", "SimboloMonetario" },
                values: new object[] { ConstantesMoneda.Euro, "EUR" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaObjetivoDeGasto");

            migrationBuilder.DropTable(
                name: "EspacioUsuario");

            migrationBuilder.DropTable(
                name: "TiposDeCambios");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "ObjetivoDeGastos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropTable(
                name: "Monedas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
