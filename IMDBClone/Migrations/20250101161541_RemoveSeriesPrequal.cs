using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDBClone.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeriesPrequal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Serieses_Serieses_SeriesPrequelId",
                table: "Serieses");

            migrationBuilder.DropIndex(
                name: "IX_Serieses_SeriesPrequelId",
                table: "Serieses");

            migrationBuilder.DropColumn(
                name: "SeriesPrequelId",
                table: "Serieses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SeriesPrequelId",
                table: "Serieses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Serieses_SeriesPrequelId",
                table: "Serieses",
                column: "SeriesPrequelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Serieses_Serieses_SeriesPrequelId",
                table: "Serieses",
                column: "SeriesPrequelId",
                principalTable: "Serieses",
                principalColumn: "Id");
        }
    }
}
