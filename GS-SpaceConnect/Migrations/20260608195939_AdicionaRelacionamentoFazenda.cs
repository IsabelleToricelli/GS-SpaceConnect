using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS_SpaceConnect.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaRelacionamentoFazenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropriedadeRuralId",
                table: "AnaliseAgricolas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnaliseAgricolas_PropriedadeRuralId",
                table: "AnaliseAgricolas",
                column: "PropriedadeRuralId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnaliseAgricolas_PropriedadesRurais_PropriedadeRuralId",
                table: "AnaliseAgricolas",
                column: "PropriedadeRuralId",
                principalTable: "PropriedadesRurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnaliseAgricolas_PropriedadesRurais_PropriedadeRuralId",
                table: "AnaliseAgricolas");

            migrationBuilder.DropIndex(
                name: "IX_AnaliseAgricolas_PropriedadeRuralId",
                table: "AnaliseAgricolas");

            migrationBuilder.DropColumn(
                name: "PropriedadeRuralId",
                table: "AnaliseAgricolas");
        }
    }
}
