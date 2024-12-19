using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDBClone.Migrations
{
    /// <inheritdoc />
    public partial class editSelfRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Episodes_EpisodePrequelId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Movies_MoviePrequelId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Serieses_Serieses_SeriesPrequelId",
                table: "Serieses");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeriesPrequelId",
                table: "Serieses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MoviePrequelId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpisodePrequelId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Episodes_EpisodePrequelId",
                table: "Episodes",
                column: "EpisodePrequelId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Movies_MoviePrequelId",
                table: "Movies",
                column: "MoviePrequelId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Serieses_Serieses_SeriesPrequelId",
                table: "Serieses",
                column: "SeriesPrequelId",
                principalTable: "Serieses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Episodes_EpisodePrequelId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Movies_MoviePrequelId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Serieses_Serieses_SeriesPrequelId",
                table: "Serieses");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeriesPrequelId",
                table: "Serieses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MoviePrequelId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpisodePrequelId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Episodes_EpisodePrequelId",
                table: "Episodes",
                column: "EpisodePrequelId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Movies_MoviePrequelId",
                table: "Movies",
                column: "MoviePrequelId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Serieses_Serieses_SeriesPrequelId",
                table: "Serieses",
                column: "SeriesPrequelId",
                principalTable: "Serieses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
