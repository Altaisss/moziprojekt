using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Felhasznalok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nev = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Jelszo = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Felhasznalok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hossz = table.Column<int>(type: "INTEGER", nullable: false),
                    Leiras = table.Column<string>(type: "TEXT", nullable: true),
                    Cim = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Rendezo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Termek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeremNev = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foglalasok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FelhasznaloId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foglalasok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foglalasok_Felhasznalok_FelhasznaloId",
                        column: x => x.FelhasznaloId,
                        principalTable: "Felhasznalok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Szekek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sor = table.Column<int>(type: "INTEGER", nullable: false),
                    Szam = table.Column<int>(type: "INTEGER", nullable: false),
                    Oldal = table.Column<char>(type: "TEXT", nullable: false),
                    TeremId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szekek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Szekek_Termek_TeremId",
                        column: x => x.TeremId,
                        principalTable: "Termek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vetitesek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Idopont = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeremId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vetitesek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vetitesek_Filmek_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vetitesek_Termek_TeremId",
                        column: x => x.TeremId,
                        principalTable: "Termek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Foglalthelyek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SzekId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoglalasId = table.Column<int>(type: "INTEGER", nullable: false),
                    VetitesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foglalthelyek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foglalthelyek_Foglalasok_FoglalasId",
                        column: x => x.FoglalasId,
                        principalTable: "Foglalasok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foglalthelyek_Szekek_SzekId",
                        column: x => x.SzekId,
                        principalTable: "Szekek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foglalthelyek_Vetitesek_VetitesId",
                        column: x => x.VetitesId,
                        principalTable: "Vetitesek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Felhasznalo_Email_Unique",
                table: "Felhasznalok",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Film_Cim",
                table: "Filmek",
                column: "Cim");

            migrationBuilder.CreateIndex(
                name: "IX_Foglalas_FelhasznaloId",
                table: "Foglalasok",
                column: "FelhasznaloId");

            migrationBuilder.CreateIndex(
                name: "IX_Foglalthely_Vetites_Szek_Unique",
                table: "Foglalthelyek",
                columns: new[] { "VetitesId", "SzekId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foglalthelyek_FoglalasId",
                table: "Foglalthelyek",
                column: "FoglalasId");

            migrationBuilder.CreateIndex(
                name: "IX_Foglalthelyek_SzekId",
                table: "Foglalthelyek",
                column: "SzekId");

            migrationBuilder.CreateIndex(
                name: "IX_Szekek_TeremId",
                table: "Szekek",
                column: "TeremId");

            migrationBuilder.CreateIndex(
                name: "IX_Vetites_FilmId_Idopont",
                table: "Vetitesek",
                columns: new[] { "FilmId", "Idopont" });

            migrationBuilder.CreateIndex(
                name: "IX_Vetites_Idopont",
                table: "Vetitesek",
                column: "Idopont");

            migrationBuilder.CreateIndex(
                name: "IX_Vetites_TeremId_Idopont",
                table: "Vetitesek",
                columns: new[] { "TeremId", "Idopont" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foglalthelyek");

            migrationBuilder.DropTable(
                name: "Foglalasok");

            migrationBuilder.DropTable(
                name: "Szekek");

            migrationBuilder.DropTable(
                name: "Vetitesek");

            migrationBuilder.DropTable(
                name: "Felhasznalok");

            migrationBuilder.DropTable(
                name: "Filmek");

            migrationBuilder.DropTable(
                name: "Termek");
        }
    }
}
