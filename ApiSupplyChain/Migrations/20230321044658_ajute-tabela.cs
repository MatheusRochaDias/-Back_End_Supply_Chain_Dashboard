using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSupplyChain.Migrations
{
    /// <inheritdoc />
    public partial class ajutetabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Estoque_ProdutoId",
                table: "Movimentacao");

            migrationBuilder.RenameColumn(
                name: "Tipo_De_Movimentacao",
                table: "Movimentacao",
                newName: "TipoMovimentacao");

            migrationBuilder.RenameColumn(
                name: "Quantidade_Movimentada",
                table: "Movimentacao",
                newName: "QuantidadeMovimentada");

            migrationBuilder.RenameColumn(
                name: "Data_Evento",
                table: "Movimentacao",
                newName: "DataEvento");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Estoque",
                newName: "Quantidade");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Produtos_ProdutoId",
                table: "Movimentacao",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Produtos_ProdutoId",
                table: "Movimentacao");

            migrationBuilder.RenameColumn(
                name: "TipoMovimentacao",
                table: "Movimentacao",
                newName: "Tipo_De_Movimentacao");

            migrationBuilder.RenameColumn(
                name: "QuantidadeMovimentada",
                table: "Movimentacao",
                newName: "Quantidade_Movimentada");

            migrationBuilder.RenameColumn(
                name: "DataEvento",
                table: "Movimentacao",
                newName: "Data_Evento");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Estoque",
                newName: "Quantity");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Estoque_ProdutoId",
                table: "Movimentacao",
                column: "ProdutoId",
                principalTable: "Estoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
