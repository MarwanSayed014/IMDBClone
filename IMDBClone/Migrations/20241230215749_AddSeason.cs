using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDBClone.Migrations
{
    /// <inheritdoc />
    public partial class AddSeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Serieses_SeriesId",
                table: "Episodes");

            migrationBuilder.RenameColumn(
                name: "SeriesId",
                table: "Episodes",
                newName: "SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_SeriesId",
                table: "Episodes",
                newName: "IX_Episodes_SeasonId");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonPrequelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Seasons_SeasonPrequelId",
                        column: x => x.SeasonPrequelId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Seasons_Serieses_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Serieses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SeasonPrequelId",
                table: "Seasons",
                column: "SeasonPrequelId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SeriesId",
                table: "Seasons",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Episodes");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "Episodes",
                newName: "SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                newName: "IX_Episodes_SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Serieses_SeriesId",
                table: "Episodes",
                column: "SeriesId",
                principalTable: "Serieses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
