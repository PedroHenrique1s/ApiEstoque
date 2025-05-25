using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class MigraçãoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaCategorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaCategorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaProdutos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaProdutos", x => x.id);
                    table.ForeignKey(
                        name: "FK_TabelaProdutos_TabelaCategorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "TabelaCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaProdutos_CategoriaId",
                table: "TabelaProdutos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaProdutos");

            migrationBuilder.DropTable(
                name: "TabelaUsuarios");

            migrationBuilder.DropTable(
                name: "TabelaCategorias");
        }
    }
}
