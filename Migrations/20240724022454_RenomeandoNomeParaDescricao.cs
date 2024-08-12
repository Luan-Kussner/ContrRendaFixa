using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContrRendaFixa.Migrations
{
    /// <inheritdoc />
    public partial class RenomeandoNomeParaDescricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TiposProdutos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Produtos",
                newName: "Descricao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TiposProdutos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Produtos",
                newName: "Nome");
        }
    }
}
