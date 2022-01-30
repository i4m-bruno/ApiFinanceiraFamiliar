using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Infrastructure.Migrations
{
    public partial class DespesaCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Despesas",
                type: "float(12)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Despesas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Outras");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Despesas");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Despesas",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(12)",
                oldPrecision: 12,
                oldScale: 2);
        }
    }
}
