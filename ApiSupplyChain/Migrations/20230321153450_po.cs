using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSupplyChain.Migrations
{
    /// <inheritdoc />
    public partial class po : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Estoque",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Local",
                table: "Estoque");
        }
    }
}
