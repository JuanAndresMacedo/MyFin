using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memoria.Migrations
{
    /// <inheritdoc />
    public partial class AgregarObjetivosDeGastos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObjetivoDeGastoId",
                table: "Categorias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ObjetivoDeGastos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoMaximo = table.Column<float>(type: "real", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_ObjetivoDeGastoId",
                table: "Categorias",
                column: "ObjetivoDeGastoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoDeGastos_EspacioId",
                table: "ObjetivoDeGastos",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoDeGastos_UsuarioCreadorId",
                table: "ObjetivoDeGastos",
                column: "UsuarioCreadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_ObjetivoDeGastos_ObjetivoDeGastoId",
                table: "Categorias",
                column: "ObjetivoDeGastoId",
                principalTable: "ObjetivoDeGastos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_ObjetivoDeGastos_ObjetivoDeGastoId",
                table: "Categorias");

            migrationBuilder.DropTable(
                name: "ObjetivoDeGastos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_ObjetivoDeGastoId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ObjetivoDeGastoId",
                table: "Categorias");
        }
    }
}
