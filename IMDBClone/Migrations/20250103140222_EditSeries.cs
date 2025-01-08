using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDBClone.Migrations
{
    /// <inheritdoc />
    public partial class EditSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Serieses_Actors_ActorId",
                table: "Serieses");

            migrationBuilder.DropIndex(
                name: "IX_Serieses_ActorId",
                table: "Serieses");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Serieses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActorId",
                table: "Serieses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Serieses_ActorId",
                table: "Serieses",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Serieses_Actors_ActorId",
                table: "Serieses",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
