using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memoria.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCuentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monetarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<float>(type: "real", nullable: true),
                    PropietarioId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monetarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monetarias_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Monetarias_Usuarios_PropietarioId",
                        column: x => x.PropietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarjetaDeCreditos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDeCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BancoEmisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimosCuatroDigitos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditoDisponible = table.Column<float>(type: "real", nullable: true),
                    PropietarioId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetaDeCreditos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarjetaDeCreditos_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarjetaDeCreditos_Usuarios_PropietarioId",
                        column: x => x.PropietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monetarias_EspacioId",
                table: "Monetarias",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Monetarias_PropietarioId",
                table: "Monetarias",
                column: "PropietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetaDeCreditos_EspacioId",
                table: "TarjetaDeCreditos",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetaDeCreditos_PropietarioId",
                table: "TarjetaDeCreditos",
                column: "PropietarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monetarias");

            migrationBuilder.DropTable(
                name: "TarjetaDeCreditos");
        }
    }
}
