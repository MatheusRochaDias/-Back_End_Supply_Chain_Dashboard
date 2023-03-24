using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSupplyChain.Migrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Local",
                table: "Estoque");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Movimentacao",
                newName: "Tipo_De_Movimentacao");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Movimentacao",
                newName: "Quantidade_Movimentada");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Movimentacao",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Estoque",
                newName: "ProdutoId");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Movimentacao",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_ProdutoId",
                table: "Movimentacao",
                column: "ProdutoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Estoque_ProdutoId",
                table: "Movimentacao",
                column: "ProdutoId",
                principalTable: "Estoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Estoque_ProdutoId",
                table: "Movimentacao");

            migrationBuilder.DropIndex(
                name: "IX_Movimentacao_ProdutoId",
                table: "Movimentacao");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "Movimentacao");

            migrationBuilder.RenameColumn(
                name: "Tipo_De_Movimentacao",
                table: "Movimentacao",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "Quantidade_Movimentada",
                table: "Movimentacao",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Movimentacao",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Estoque",
                newName: "ProductId");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Estoque",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
