using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContrRendaFixa.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContratanteSegmentoEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Segmento",
                table: "Contratantes",
                type: "text",
                nullable: false,
                comment: "V = Varejo, A = Atacado, E = Especial",
                oldClrType: typeof(char),
                oldType: "character(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Segmento",
                table: "Contratantes",
                type: "character(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "V = Varejo, A = Atacado, E = Especial");
        }
    }
}
