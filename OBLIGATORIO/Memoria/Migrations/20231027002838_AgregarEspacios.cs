using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memoria.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEspacios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EspacioId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Espacios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministradorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Espacios_Usuarios_AdministradorId",
                        column: x => x.AdministradorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EspacioId",
                table: "Usuarios",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_AdministradorId",
                table: "Espacios",
                column: "AdministradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Espacios_EspacioId",
                table: "Usuarios",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Espacios_EspacioId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EspacioId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EspacioId",
                table: "Usuarios");
        }
    }
}
