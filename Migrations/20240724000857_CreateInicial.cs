using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ContrRendaFixa.Migrations
{
    /// <inheritdoc />
    public partial class CreateInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Sobrenome = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Segmento = table.Column<char>(type: "character(1)", nullable: false),
                    Bloqueado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProdutos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TipoId = table.Column<int>(type: "integer", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Bloqueado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_TiposProdutos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContratanteId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    DataContratacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratacoes_Contratantes_ContratanteId",
                        column: x => x.ContratanteId,
                        principalTable: "Contratantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratacoes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContratacaoId = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Contratacoes_ContratacaoId",
                        column: x => x.ContratacaoId,
                        principalTable: "Contratacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_ContratanteId",
                table: "Contratacoes",
                column: "ContratanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_ProdutoId",
                table: "Contratacoes",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_ContratacaoId",
                table: "Pagamentos",
                column: "ContratacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_TipoId",
                table: "Produtos",
                column: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Contratacoes");

            migrationBuilder.DropTable(
                name: "Contratantes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "TiposProdutos");
        }
    }
}
