using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDBClone.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReleaseYearFromEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Episodes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
