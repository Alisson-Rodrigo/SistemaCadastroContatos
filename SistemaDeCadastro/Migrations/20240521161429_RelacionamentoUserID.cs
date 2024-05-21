using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeCadastro.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Contatos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_UsuarioID",
                table: "Contatos",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Usuarios_UsuarioID",
                table: "Contatos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Usuarios_UsuarioID",
                table: "Contatos");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_UsuarioID",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Contatos");
        }
    }
}
