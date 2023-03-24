using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSupplyChain.Migrations
{
    /// <inheritdoc />
    public partial class clas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Produtos_ProdutoId",
                table: "Movimentacao");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Movimentacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Produtos_ProdutoId",
                table: "Movimentacao",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Produtos_ProdutoId",
                table: "Movimentacao");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Movimentacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Produtos_ProdutoId",
                table: "Movimentacao",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
