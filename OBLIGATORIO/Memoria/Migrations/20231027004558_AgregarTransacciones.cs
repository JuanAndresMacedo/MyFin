using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memoria.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTransacciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaDeCreditos_Espacios_EspacioId",
                table: "TarjetaDeCreditos");

            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaDeCreditos_Usuarios_PropietarioId",
                table: "TarjetaDeCreditos");

            migrationBuilder.DropTable(
                name: "Monetarias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarjetaDeCreditos",
                table: "TarjetaDeCreditos");

            migrationBuilder.RenameTable(
                name: "TarjetaDeCreditos",
                newName: "Cuenta");

            migrationBuilder.RenameIndex(
                name: "IX_TarjetaDeCreditos_PropietarioId",
                table: "Cuenta",
                newName: "IX_Cuenta_PropietarioId");

            migrationBuilder.RenameIndex(
                name: "IX_TarjetaDeCreditos_EspacioId",
                table: "Cuenta",
                newName: "IX_Cuenta_EspacioId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDeCierre",
                table: "Cuenta",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Cuenta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Monto",
                table: "Cuenta",
                type: "real",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuenta",
                table: "Cuenta",
                column: "Id");

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
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Espacios_EspacioId",
                table: "Cuenta",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Usuarios_PropietarioId",
                table: "Cuenta",
                column: "PropietarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Espacios_EspacioId",
                table: "Cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Usuarios_PropietarioId",
                table: "Cuenta");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuenta",
                table: "Cuenta");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Cuenta");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Cuenta");

            migrationBuilder.RenameTable(
                name: "Cuenta",
                newName: "TarjetaDeCreditos");

            migrationBuilder.RenameIndex(
                name: "IX_Cuenta_PropietarioId",
                table: "TarjetaDeCreditos",
                newName: "IX_TarjetaDeCreditos_PropietarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Cuenta_EspacioId",
                table: "TarjetaDeCreditos",
                newName: "IX_TarjetaDeCreditos_EspacioId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDeCierre",
                table: "TarjetaDeCreditos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TarjetaDeCreditos",
                table: "TarjetaDeCreditos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Monetarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    PropietarioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<float>(type: "real", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Monetarias_EspacioId",
                table: "Monetarias",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Monetarias_PropietarioId",
                table: "Monetarias",
                column: "PropietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaDeCreditos_Espacios_EspacioId",
                table: "TarjetaDeCreditos",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaDeCreditos_Usuarios_PropietarioId",
                table: "TarjetaDeCreditos",
                column: "PropietarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
