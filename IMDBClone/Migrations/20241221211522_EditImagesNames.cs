using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDBClone.Migrations
{
    /// <inheritdoc />
    public partial class EditImagesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImgUrl",
                table: "Users",
                newName: "ProfileImgPath");

            migrationBuilder.RenameColumn(
                name: "CoverImgUrl",
                table: "Serieses",
                newName: "CoverImgPath");

            migrationBuilder.RenameColumn(
                name: "ProfileImgUrl",
                table: "Producers",
                newName: "ProfileImgPath");

            migrationBuilder.RenameColumn(
                name: "CoverImgUrl",
                table: "Movies",
                newName: "CoverImgPath");

            migrationBuilder.RenameColumn(
                name: "CoverImgUrl",
                table: "Episodes",
                newName: "CoverImgPath");

            migrationBuilder.RenameColumn(
                name: "ProfileImgUrl",
                table: "Directors",
                newName: "ProfileImgPath");

            migrationBuilder.RenameColumn(
                name: "ProfileImgUrl",
                table: "Actors",
                newName: "ProfileImgPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImgPath",
                table: "Users",
                newName: "ProfileImgUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImgPath",
                table: "Serieses",
                newName: "CoverImgUrl");

            migrationBuilder.RenameColumn(
                name: "ProfileImgPath",
                table: "Producers",
                newName: "ProfileImgUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImgPath",
                table: "Movies",
                newName: "CoverImgUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImgPath",
                table: "Episodes",
                newName: "CoverImgUrl");

            migrationBuilder.RenameColumn(
                name: "ProfileImgPath",
                table: "Directors",
                newName: "ProfileImgUrl");

            migrationBuilder.RenameColumn(
                name: "ProfileImgPath",
                table: "Actors",
                newName: "ProfileImgUrl");
        }
    }
}
