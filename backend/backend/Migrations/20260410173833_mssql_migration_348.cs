using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_348 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foglalthelyek_Foglalasok_FoglalasId",
                table: "Foglalthelyek");

            migrationBuilder.AddColumn<string>(
                name: "KepUrl",
                table: "Filmek",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foglalthelyek_Foglalasok_FoglalasId",
                table: "Foglalthelyek",
                column: "FoglalasId",
                principalTable: "Foglalasok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foglalthelyek_Foglalasok_FoglalasId",
                table: "Foglalthelyek");

            migrationBuilder.DropColumn(
                name: "KepUrl",
                table: "Filmek");

            migrationBuilder.AddForeignKey(
                name: "FK_Foglalthelyek_Foglalasok_FoglalasId",
                table: "Foglalthelyek",
                column: "FoglalasId",
                principalTable: "Foglalasok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
