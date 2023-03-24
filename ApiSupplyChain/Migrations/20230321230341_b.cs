using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSupplyChain.Migrations
{
    /// <inheritdoc />
    public partial class b : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque");

            migrationBuilder.AlterColumn<string>(
                name: "ProdutoId",
                table: "Estoque",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId1",
                table: "Estoque",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_ProdutoId1",
                table: "Estoque",
                column: "ProdutoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId1",
                table: "Estoque",
                column: "ProdutoId1",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId1",
                table: "Estoque");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoId1",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "ProdutoId1",
                table: "Estoque");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Estoque",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId",
                table: "Estoque",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
