using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContrRendaFixa.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContratanteSegmentoToSegEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Segmento",
                table: "Contratantes",
                type: "segmento_enum",
                nullable: false,
                comment: "V = Varejo, A = Atacado, E = Especial",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "V = Varejo, A = Atacado, E = Especial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Segmento",
                table: "Contratantes",
                type: "text",
                nullable: false,
                comment: "V = Varejo, A = Atacado, E = Especial",
                oldClrType: typeof(int),
                oldType: "segmento_enum",
                oldComment: "V = Varejo, A = Atacado, E = Especial");
        }
    }
}
